using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace ServiceBus
{
    public interface IServiceBus 
    {
        void Initialize();
        SortedList<int, IComponent> Pipeline { get; set; }
        IComponent CurrentComponent { get; }
        dynamic State { get; }
        void Execute();
    }
}
