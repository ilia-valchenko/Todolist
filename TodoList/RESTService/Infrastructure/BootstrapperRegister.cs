using Microsoft.Practices.Unity;
using Bootstrap.Unity;
using DAL.Interfaces.Repository.ModelRepository;
using DAL.Concrete;
using BLL.Interfaces.Services.EntityService;
using BLL.Concrete;

namespace RESTService.Infrastructure
{
    public class BootstrapperRegister : IUnityRegistration
    {
        public void Register(IUnityContainer container)
        {    
            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IElasticClient, ElasticClient>(new HierarchicalLifetimeManager());
            container.RegisterType<IElasticRepository, ElasticRepository>(new HierarchicalLifetimeManager());          
        }
    }
}