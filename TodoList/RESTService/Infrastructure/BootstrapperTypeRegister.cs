using System;
using Microsoft.Practices.Unity;
using Bootstrap.Unity;
using DAL.Repositories.Interfaces;
using DAL.Concrete;
using BLL.Services.Interfaces;
using BLL.Services.Concrete;
using DAL.Repositories.Concrete;
using Nest;
using Elasticsearch.Net;

namespace RESTService.Infrastructure
{
    public class BootstrapperTypeRegister : IUnityRegistration
    {
        public void Register(IUnityContainer container)
        {    
            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IElasticRepository, ElasticRepository>(new HierarchicalLifetimeManager());

            //container.RegisterType<IElasticClient, ElasticClient>(new HierarchicalLifetimeManager());
            //container.RegisterType<IConnectionSettingsValues, ConnectionSettings>(new InjectionConstructor(new Uri("http://localhost:9200")));
        }
    }
}