namespace DependencyInjectionDemo.WebApi.IocTest
{
    public interface IScopedService : IScopedDependency
    {
        Guid GetGuid();
    }

    public class ScopedService : IScopedService
    {
        private readonly Guid guid;
        public ScopedService()
        {
            guid = Guid.NewGuid();
        }

        public Guid GetGuid()
        {
            return guid;
        }
    }


    public interface ISingletonService : ISingletonDependency
    {
        Guid GetGuid();
    }

    public class SingletonService : ISingletonService
    {
        private readonly Guid guid;
        public SingletonService()
        {
            guid = Guid.NewGuid();
        }

        public Guid GetGuid()
        {
            return guid;
        }
    }


    public interface ITransientService: ITransientDependency
    {
        Guid GetGuid();
    }

    public class TransientService : ITransientService
    {
        private readonly Guid guid;
        public TransientService()
        {
            guid = Guid.NewGuid();
        }

        public Guid GetGuid()
        {
            return guid;
        }
    }
}
