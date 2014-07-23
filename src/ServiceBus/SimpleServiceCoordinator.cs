using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Dynamic;

namespace ServiceBus
{
    public class SimpleServiceCoordinator : IServiceCoordinator
    {
        public virtual CoordinatorResult ExecuteComponent(IComponent component, object state)
        {
            MethodInfo method = DetermineMethod(component);
            var arguments = DetermineArguments(method, (object)state);
            var res = method.Invoke(component, arguments.Select(t => t.Value).ToArray());

            if (res == null)
                throw new NullComponentResultException(method.DeclaringType.Name);

            var props = DetermineResult(res);
            foreach (var p in props)
            {
                var prop = state.GetType().GetProperty(p.Key);
                if (prop != null)
                    prop.SetValue(state, p.Value);
                else
                    throw new MissingMemberException(string.Format("Error while accessing property {0}", p.Key));
            }
            return null;
        }

        public virtual MethodInfo DetermineMethod(IComponent component)
        {
            if (component == null) throw new ArgumentNullException("component");
            var mi = component.GetType().GetMethods().Where(m => m.IsPublic && m.Name == "Execute" && m.GetCustomAttribute(typeof(ComponentActionAttribute)) != null);
            if (!mi.Any())
                throw new MissingMethodException("No method Execute with custom attribute ComponentAction found on {0}", component.GetType().FullName);
            else
                return mi.First();
        }

        public virtual IDictionary<string, object> DetermineArguments(MethodInfo method, dynamic state)
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();
            var args = method.GetParameters().Select(t => t.Name).ToList();
            args.ForEach(t =>
            {
                dict.Add(t, state.GetType().GetProperty(t, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(state));
            });
            return dict;
        }

        public virtual IDictionary<string, object> DetermineResult(object result)
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();
            result.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).ToList().ForEach(t =>
            {
                dict.Add(t.Name, t.GetValue(result));
            });
            return dict;
        }

    }
}
