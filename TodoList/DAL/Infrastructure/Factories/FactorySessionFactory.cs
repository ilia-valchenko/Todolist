using NHibernate;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using DAL.Entities;

//custom
//using System.Configuration;

namespace DAL.Infrastructure.Factories.Interfacies
{
    public class FactorySessionFactory  : IFactorySessionFactory
    {
        public ISessionFactory CreateSessionFactory()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                                             .Database(MsSqlConfiguration.MsSql2012.ConnectionString(/*ConfigurationManager.AppSettings["connectionString"]*/@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Taskstorage.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").ShowSql())
                                             .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskEntityMap>())
                                             .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                                             .BuildSessionFactory();

            return sessionFactory;
        }
    }
}