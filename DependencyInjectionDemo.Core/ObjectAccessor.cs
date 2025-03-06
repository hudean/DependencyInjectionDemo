using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core
{
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        public T? Value { get; set; }

        public ObjectAccessor()
        {

        }

        public ObjectAccessor(T? obj)
        {
            Value = obj;
        }
    }
}
