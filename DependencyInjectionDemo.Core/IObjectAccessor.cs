using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core
{
    public interface IObjectAccessor<out T>
    {
        T? Value { get; }
    }

}
