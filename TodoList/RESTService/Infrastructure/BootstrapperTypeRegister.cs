using System;
using Microsoft.Practices.Unity;
using Bootstrap.Unity;
using DAL.Repositories.Interfaces;
using DAL.Concrete;
using BLL.Services.Interfaces;
using BLL.Services.Concrete;
using DAL.Repositories.Concrete;
using Nest;
using FluentNHibernate.Cfg;
using DAL.Entities;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using NHibernate;

namespace RESTService.Infrastructure
{
    public class BootstrapperTypeRegister : IUnityRegistration
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterInstance(typeof(ISessionFactory),
                                       Fluently.Configure()
                                               .Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConfigurationManager.AppSettings["connectionString"]).ShowSql())
                                               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DalTaskMap>())
                                               .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                                               .BuildSessionFactory(),
                                       new ContainerControlledLifetimeManager());

            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IElasticRepository, ElasticRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaskService, TaskService>(new HierarchicalLifetimeManager());
            
            container.RegisterType<IElasticClient, ElasticClient>(new ContainerControlledLifetimeManager(), 
                                                                  new InjectionConstructor(
                                                                      new ConnectionSettings(
                                                                            new Uri(ConfigurationManager.AppSettings["elasticSearchUri"])
                                                                      )
                                                                      .DefaultIndex(ConfigurationManager.AppSettings["defaultIndex"])
                                                                      .DefaultTypeNameInferrer(type => ConfigurationManager.AppSettings["defaultType"])
                                                                 ));
        }
    }
}
