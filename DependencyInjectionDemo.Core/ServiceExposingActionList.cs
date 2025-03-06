using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core
{
    public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
    {

    }
}
