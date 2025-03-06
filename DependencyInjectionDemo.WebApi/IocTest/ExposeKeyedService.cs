namespace DependencyInjectionDemo.WebApi.IocTest
{
    public interface IExposeKeyedService:ITransientDependency
    {
        Guid GetGuid();
    }

    [ExposeKeyedService<IExposeKeyedService>("k1")]
    [ExposeKeyedService<ExposeKeyedService>("k1")]
    public class ExposeKeyedService: IExposeKeyedService
    {
        private readonly Guid guid;
        public ExposeKeyedService()
        {
            guid = Guid.CreateVersion7();
        }

        public  Guid GetGuid()
        {
            return guid;
        }
    }

    [ExposeKeyedService<IExposeKeyedService>("k2")]
    [ExposeKeyedService<TwoExposeKeyedService>("k2")]
    public class TwoExposeKeyedService : IExposeKeyedService
    {
        private readonly Guid guid;
        public TwoExposeKeyedService()
        {
            guid = Guid.CreateVersion7();
        }

        public Guid GetGuid()
        {
            return guid;
        }
    }


    public interface IExposeService
    {
        Guid GetGuid();
    }

    [ExposeServices(typeof(IExposeService), typeof(ExposeService))]
    public class ExposeService : IExposeService
    {
        private readonly Guid guid;
        public ExposeService()
        {
            guid = Guid.CreateVersion7();
        }

        public Guid GetGuid()
        {
            return guid;
        }
    }


    public interface IMySingletonExposingMultipleServices
    {

    }

    [ExposeServices(typeof(IMySingletonExposingMultipleServices), typeof(MySingletonExposingMultipleServices))]
    [ExposeKeyedService<IMySingletonExposingMultipleServices>("k1")]
    [ExposeKeyedService<MySingletonExposingMultipleServices>("k1")]
    public class MySingletonExposingMultipleServices : IMySingletonExposingMultipleServices, ISingletonDependency
    {

    }
}
