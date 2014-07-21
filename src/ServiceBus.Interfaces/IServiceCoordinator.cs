using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public interface IServiceCoordinator
    {

        SortedList<string, IComponent> Pipeline { get; }

    }
}
