using Microsoft.Practices.Unity;
using Bootstrap.Unity;
using DAL.Repositories.Interfaces;
using DAL.Concrete;
using BLL.Services.Interfaces;
using BLL.Services.Concrete;
using DAL.Repositories.Concrete;
using Nest;
using Elasticsearch.Net;
using FluentNHibernate.Cfg;
using DAL.Entities;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg.Db;
using System.Configuration;

namespace RESTService.Infrastructure
{
    public class BootstrapperTypeRegister : IUnityRegistration
    {
        public void Register(IUnityContainer container)
        {    
            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager(), 
                                                                    new InjectionConstructor(
                                                                        Fluently.Configure()
                                                                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConfigurationManager.AppSettings["connectionString"]).ShowSql())
                                                                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DalTaskMap>())
                                                                        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                                                                        .BuildSessionFactory()));

            container.RegisterType<IElasticRepository, ElasticRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaskService, TaskService>(new ContainerControlledLifetimeManager());
            

            //container.RegisterType<IElasticClient, ElasticClient>(new HierarchicalLifetimeManager());
            //container.RegisterType<IConnectionSettingsValues, ConnectionSettings>(new InjectionConstructor(new Uri("http://localhost:9200")));
        }
    }
}