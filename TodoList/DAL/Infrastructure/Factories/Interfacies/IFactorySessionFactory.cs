using NHibernate;

namespace DAL.Infrastructure.Factories.Interfacies
{
    public interface IFactorySessionFactory
    {
        ISessionFactory CreateSessionFactory();
    }
}
