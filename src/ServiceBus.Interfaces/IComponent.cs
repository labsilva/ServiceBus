using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public interface IComponent<TInput, TOutput>
    {

        bool Parallel { get; set; }


    }
}
