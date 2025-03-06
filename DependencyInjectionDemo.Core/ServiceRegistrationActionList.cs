using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core
{
    public class ServiceRegistrationActionList : List<Action<IOnServiceRegistredContext>>
    {
        public bool IsClassInterceptorsDisabled { get; set; }
    }
}
