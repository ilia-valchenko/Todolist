using Nest;

namespace DAL.Infrastructure.Factories.Interfacies
{
    public interface IElasticClientFactory
    {
        IElasticClient CreateElasticClient();
    }
}
