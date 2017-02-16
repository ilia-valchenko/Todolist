using Nest;
using NHibernate;
using Microsoft.Practices.Unity;
using DAL.Infrastructure.Factories;
using DAL.Infrastructure.Factories.Interfacies;

namespace DAL.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IElasticClientFactory, ElasticClientFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<IElasticClient>(new InjectionFactory((c) => c.Resolve<IElasticClientFactory>().CreateElasticClient()));

            container.RegisterType<IFactorySessionFactory, FactorySessionFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISessionFactory>(new InjectionFactory((c) => c.Resolve<IFactorySessionFactory>().CreateSessionFactory()));
        }
    }
}
