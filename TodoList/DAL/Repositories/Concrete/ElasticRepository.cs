using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Nest;
using Elasticsearch.Net;

namespace DAL.Repositories.Concrete
{
    public class ElasticRepository : IElasticRepository
    {
        private readonly IElasticClient elasticClient;

        public ElasticRepository(/*IElasticClient elasticClient*/)
        {
            //this.elasticClient = elasticClient;

            const string indexName = "taskmanager";
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("taskmanager").DefaultTypeNameInferrer(t => "task");

            elasticClient = new ElasticClient(settings);

            IndexSettings indexSettings = new IndexSettings();


            CustomAnalyzer customAnalyzer = new CustomAnalyzer();
            customAnalyzer.CharFilter = new List<string>();
            customAnalyzer.Tokenizer = "mynGram";
            customAnalyzer.Filter = new List<string> { "lowercase" };


            indexSettings.Analysis = new Analysis();
            indexSettings.Analysis.Analyzers = new Analyzers();
            indexSettings.Analysis.Tokenizers = new Tokenizers();


            indexSettings.Analysis.Analyzers.Add("mynGram", customAnalyzer);
            indexSettings.Analysis.Tokenizers.Add("mynGram", new NGramTokenizer { MaxGram = 10, MinGram = 2, });

            IndexState indexConfig = new IndexState
            {
                Settings = indexSettings
            };

            elasticClient.CreateIndex(indexName, i => i
                .InitializeUsing(indexConfig)
            );
        }

        #region CRUD
        public void Create(DalTask entity)
        {
            try
            {
                elasticClient.Index(entity);
            }
            catch (ElasticsearchClientException clientException)
            {
                // ILogger
                throw;
            }
            catch(Exception exception)
            {
                // ILogger
                throw;
            }
        }

        public void Update(DalTask entity)
        {
            try
            {
                elasticClient.Update<DalTask>(entity.Id, u => u.Doc(entity));
            }
            catch(ElasticsearchClientException clientException)
            {
                // ILogger
                throw;
            }
            catch(Exception exception)
            {
                // ILogger
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                elasticClient.Delete<DalTask>(id.ToString());
            }
            catch(ElasticsearchClientException clientException)
            {
                // ILogger
                throw;
            }
            catch(Exception exception)
            {
                // ILogger
                throw;
            }
        }
        #endregion

        #region Get
        public IEnumerable<DalTask> GetAll()
        {
            ISearchResponse<DalTask> searchResponse;

            try
            {
                searchResponse = elasticClient.Search<DalTask>();
            }
            catch(ElasticsearchClientException clientException)
            {
                // ILogger
                throw;
            }
            catch(Exception exception)
            {
                // ILogger
                throw;
            }
            
            IEnumerable<DalTask> documents = searchResponse.Documents;
            return documents;
        } 
            

        public DalTask GetById(int id) => elasticClient?.Get<DalTask>(id.ToString()).Source;

        public IEnumerable<DalTask> GetQueryResults(string query)
        {
            ISearchResponse<DalTask> searchResponse;

            try
            {
                searchResponse =
                elasticClient.Search<DalTask>(s => s
                    .Query(q => q
                        .Bool(b => b
                            .Should(sh => sh
                                .Wildcard(w => w
                                    .Field(f => f.Title)
                                    .Value($"*{query}*")
                                ),
                                shd => shd
                                    .Match(m => m
                                        .Field(f => f.Title)
                                        .Query(query)
                                    )
                            )
                        )
                    )
                );
            }
            catch(ElasticsearchClientException clientException)
            {
                // ILogger
                throw;
            }
            catch(Exception exception)
            {
                // ILogger
                throw;
            }

            return searchResponse.Documents;
        }
        #endregion
    }
}
