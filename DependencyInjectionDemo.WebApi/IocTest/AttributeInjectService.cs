namespace DependencyInjectionDemo.WebApi.IocTest
{

    public interface IAttributeInjectService : ITransientDependency
    {
        Guid GetGuid();
    }

    [Dependency(ServiceLifetime.Transient)]
    public class AttributeInjectService: IAttributeInjectService
    {
        private readonly Guid guid;
        public AttributeInjectService()
        {
            guid = Guid.NewGuid();
        }

        public virtual Guid GetGuid()
        {
            return guid;
        }
    }
    //注意如果Dependency特性设置生命周期，而且没有实现Dependency相关接口，则注入无效因为默认的Lifetime是null
    [Dependency(ReplaceServices = true)] 
    //[Dependency(ServiceLifetime.Transient,ReplaceServices = true)]
    public class TwoAttributeInjectService: AttributeInjectService
    {
        private readonly Guid guid;
        public TwoAttributeInjectService()
        {
            guid = Guid.CreateVersion7();
        }

        public override Guid GetGuid()
        {
            return guid;
        }
    }
}
