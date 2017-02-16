using System;
using Nest;
using RESTService.Infrastructure.Factories.Interfacies;
using DAL.Entities;
using System.Configuration;

namespace RESTService.Infrastructure.Factories
{
    public class ElasticClientFactory : IElasticClientFactory
    {
        private const string indexName = "taskmanager";
        public IElasticClient CreateElasticClient()
        {
            ElasticClient client = new ElasticClient(new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["elasticSearchUri"])));

            if(!client.IndexExists(indexName).Exists)
            {
                  ICreateIndexResponse createIndexResponse = client.CreateIndex(indexName, u => u
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
                        .Map<TaskEntity>(m => m
                            .AutoMap()
                        )
                    )
               );
            }

            return client;
        }
    }
}