using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Dynamic;

namespace ServiceBus
{
    public interface IServiceCoordinator
    {
        CoordinatorResult ExecuteComponent(IComponent component, ExpandoObject state);
    }
}
