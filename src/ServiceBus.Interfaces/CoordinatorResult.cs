using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class CoordinatorResult
    {

        bool Result { get; set; }
        string ResultCode { get; set; }
        string ResultMessage { get; set; }

    }
}
