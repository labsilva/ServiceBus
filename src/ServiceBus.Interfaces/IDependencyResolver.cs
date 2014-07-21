using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public interface IDependencyResolver
    {
        object GetService(Type serviceType);
        T GetService<T>(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
        IEnumerable<T> GetServices<T>(Type serviceType);
    }
}
