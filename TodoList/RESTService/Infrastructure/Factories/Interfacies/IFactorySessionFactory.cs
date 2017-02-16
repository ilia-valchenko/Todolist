using NHibernate;

namespace RESTService.Infrastructure.Factories.Interfacies
{
    interface IFactorySessionFactory
    {
        ISessionFactory CreateSessionFactory();
    }
}
