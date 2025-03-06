using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core
{
    public interface IDependency
    {
    }

    /// <summary>
    /// 作用域依赖
    /// </summary>
    public interface IScopedDependency
    {
    }

    /// <summary>
    /// 单例依赖
    /// </summary>
    public interface ISingletonDependency
    {
    }

    /// <summary>
    /// 瞬时依赖
    /// </summary>
    public interface ITransientDependency
    {
    }
}
