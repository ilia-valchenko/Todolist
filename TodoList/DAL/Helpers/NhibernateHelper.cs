using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using DAL.Entities;

namespace DAL.Helpers
{
    public class NhibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory factory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\GitProjects\TodolistNhibernate\TodoList\DAL\App_Data\Taskstorage.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent =ReadWrite;MultiSubnetFailover=False").ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DalTaskMap>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildSessionFactory();

            return factory.OpenSession();
        }
    }
}
