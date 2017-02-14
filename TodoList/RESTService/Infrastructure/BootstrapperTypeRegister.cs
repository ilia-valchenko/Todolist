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
using Logger;

namespace RESTService.Infrastructure
{
    public sealed class BootstrapperTypeRegister : IUnityRegistration
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<ILogger, NLogLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<System.Web.Http.Filters.IFilter, HandleExceptionsAttribute>(new ContainerControlledLifetimeManager());

            ElasticClient client = new ElasticClient(new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["elasticSearchUri"])));

            ICreateIndexResponse createIndexResponse = client.CreateIndex("taskmanager", u => u
                .Settings(s => s
                    .Analysis(a => a
                        .Tokenizers(token => token
                            .NGram("customNGramTokenizer", ng => ng
                                .MinGram(1)
                                .MaxGram(15)
                                .TokenChars(TokenChar.Letter, TokenChar.Digit)
                            )
                        )
                        .Analyzers(analyzer => analyzer
                            .Custom("customIndexNgramAnalyzer", cia => cia
                                .Filters("lowercase")
                                .Tokenizer("customNGramTokenizer")
                            )
                            .Custom("customSearchNgramAnalyzer", csa => csa
                                .Filters("lowercase")
                                .Tokenizer("keyword")
                            )
                        )
                    )
                )
                .Mappings(map => map
                    .Map<DalTask>(m => m
                        .AutoMap()
                    )
                )
           );

            container.RegisterInstance<IElasticClient>(client, new ContainerControlledLifetimeManager());

            container.RegisterInstance(typeof(ISessionFactory),
                                       Fluently.Configure()
                                               .Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConfigurationManager.AppSettings["connectionString"]).ShowSql())
                                               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DalTaskMap>())
                                               .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                                               .BuildSessionFactory(),
                                       new ContainerControlledLifetimeManager());

            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IElasticRepository, ElasticRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaskService, TaskService>(new HierarchicalLifetimeManager(), new InjectionConstructor(new ResolvedParameter<ITaskRepository>(), new ResolvedParameter<IElasticRepository>(), "taskmanager"));
        }
    }
}
