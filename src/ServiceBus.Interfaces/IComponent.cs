using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public interface IComponent
    {
        bool Parallel { get; }
        //Type ContractInput { get; }
        //Type ContractOutput { get; }
        //object Execute(object input);
    }
}
