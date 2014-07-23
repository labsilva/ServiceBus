using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Dynamic;

namespace ServiceBus
{
    public class ServiceBusBaseState
    {

        public ServiceBusBaseState()
        {
            OperationGuid = Guid.NewGuid();
            OperationStartTimestamp = DateTime.Now;
        }

        public Guid OperationGuid { get; set; }
        public DateTime OperationStartTimestamp { get; set; }
        public DateTime? OperationFinishTimestamp { get; set; }
        public TimeSpan OperationDuration { get { return (OperationFinishTimestamp == null ? DateTime.Now : OperationFinishTimestamp.Value).Subtract(OperationStartTimestamp); } }

    }
}
