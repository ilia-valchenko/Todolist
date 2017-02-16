using Nest;

namespace RESTService.Infrastructure.Factories.Interfacies
{
    public interface IElasticClientFactory
    {
        IElasticClient CreateElasticClient();
    }
}
