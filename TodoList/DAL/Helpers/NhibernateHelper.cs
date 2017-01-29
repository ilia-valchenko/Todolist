using NHibernate;
using NHibernate.Cfg;
using System.Reflection;
using ORM.Models;

namespace DAL.Helpers
{
    public class NhibernateHelper
    {
        private static ISessionFactory factory;

        private static ISessionFactory Factory
        {
            get
            {
                if (factory == null)
                {
                    Configuration config = new Configuration();
                    config.Configure();
                    factory = config.BuildSessionFactory();

                }

                return factory;
            }
        }

        public static ISession OpenSession() => Factory.OpenSession();
    }
}
