using System.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using DAL.Entities;
using RESTService.Infrastructure.Factories.Interfacies;

namespace RESTService.Infrastructure.Factories
{
    public class FactorySessionFactory  : IFactorySessionFactory
    {
        public ISessionFactory CreateSessionFactory()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                                             .Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConfigurationManager.AppSettings["connectionString"]).ShowSql())
                                             .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskEntityMap>())
                                             .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                                             .BuildSessionFactory();

            return sessionFactory;
        }
    }
}