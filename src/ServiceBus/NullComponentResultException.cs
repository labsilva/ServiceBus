using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class NullComponentResultException : NullReferenceException
    {
        public NullComponentResultException(string componentName) : base(string.Format("Null response received from component {0}", componentName))
        {
            ComponentName = componentName;
        }

        public string ComponentName { get; set; }
    }
}
