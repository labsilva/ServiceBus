using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Dynamic;

namespace ServiceBus
{
    public class ServiceBusBaseState : DynamicObject
    {

        public Dictionary<string, object> Properties { get; set; }

        public ServiceBusBaseState() : this(null) { }

        public ServiceBusBaseState(Dictionary<string, object> properties)
        {
            if(properties == null) properties = new Dictionary<string,object>();
            Properties = properties;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Properties.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (Properties.ContainsKey(binder.Name))
            {
                result = Properties[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (Properties.ContainsKey(binder.Name))
            {
                Properties[binder.Name] = value;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
