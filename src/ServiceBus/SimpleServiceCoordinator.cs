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
        public CoordinatorResult ExecuteComponent(IComponent component, ExpandoObject state)
        {
            MethodInfo method = DetermineMethod(component);
            var arguments = DetermineArguments(method, (object)state);
            var res = method.Invoke(component, arguments.Select(t => t.Value).ToArray());
            var props = DetermineResult(res);
            foreach (var p in props)
            {
                (state as IDictionary<string, object>).Add(p.Key, p.Value);
            }
            return null;
        }

        public MethodInfo DetermineMethod(IComponent component)
        {
            if (component == null) throw new ArgumentNullException("component");
            var mi = component.GetType().GetMethods().Where(m => m.IsPublic && m.Name == "Execute" && m.GetCustomAttribute(typeof(ComponentActionAttribute)) != null);
            if (!mi.Any())
                throw new MissingMethodException("No method Execute with custom attribute ComponentAction found on {0}", component.GetType().FullName);
            else
                return mi.First();
        }

        public IDictionary<string, object> DetermineArguments(MethodInfo method, dynamic state)
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();
            var args = method.GetParameters().Select(t => t.Name).ToList();
            args.ForEach(t =>
            {
                if (state.GetType() == typeof(ExpandoObject))
                    dict.Add(t, ((IDictionary<string, object>)state).ToDictionary(k=> k.Key.ToLower(), v=> v.Value)[t.ToLower()]);
                else
                    dict.Add(t, state.GetType().GetProperty(t, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(state));
            });
            return dict;
        }

        public IDictionary<string, object> DetermineResult(object res)
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();
            res.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).ToList().ForEach(t =>
            {
                dict.Add(t.Name, t.GetValue(res));
            });
            return dict;
        }

    }
}
