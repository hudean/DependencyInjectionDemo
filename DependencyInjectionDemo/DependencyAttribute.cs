using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo;

/// <summary>
/// 依赖注入特性
/// </summary>
public class DependencyAttribute : Attribute
{
    /// <summary>
    /// 服务生命周期
    /// </summary>
    public virtual ServiceLifetime? Lifetime { get; set; }

    /// <summary>
    /// 尝试注册
    /// </summary>
    public virtual bool TryRegister { get; set; }

    /// <summary>
    /// 替换服务
    /// </summary>
    public virtual bool ReplaceServices { get; set; }


    public DependencyAttribute()
    {
        //Lifetime = ServiceLifetime.Transient;
    }

    public DependencyAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}
