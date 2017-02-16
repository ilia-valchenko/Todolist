using Microsoft.Practices.Unity;
using Bootstrap.Unity;
using DAL.Repositories.Interfaces;
using DAL.Repositories.DatabaseRepositories;
using DAL.Repositories.ElasticRepositories;
using BLL.Services.Interfaces;
using BLL.Services;
using NHibernate;
using Nest;
using Logger;
using RESTService.Handlers;
using RESTService.Infrastructure.Factories;
using RESTService.Infrastructure.Factories.Interfacies;


namespace RESTService.Infrastructure
{
    public sealed class BootstrapperTypeRegister : IUnityRegistration
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<ILogger, NLogLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<System.Web.Http.Filters.IFilter, HandleExceptionsAttribute>(new ContainerControlledLifetimeManager());
            container.RegisterType<IElasticClientFactory, ElasticClientFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<IElasticClient>(new InjectionFactory((c) => c.Resolve<IElasticClientFactory>().CreateElasticClient()));
            container.RegisterType<IFactorySessionFactory, FactorySessionFactory>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISessionFactory>(new InjectionFactory((c) => c.Resolve<IFactorySessionFactory>().CreateSessionFactory()));
            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IElasticRepository, ElasticRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaskService, TaskService>(new HierarchicalLifetimeManager(), new InjectionConstructor(new ResolvedParameter<ITaskRepository>(), new ResolvedParameter<IElasticRepository>(), "taskmanager"));
        }
    }
}
