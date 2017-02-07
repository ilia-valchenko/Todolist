using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Nest;

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
            IIndexResponse indexResponse = elasticClient.Index(entity);
        }

        public void Update(DalTask entity)
        {
            IUpdateResponse<DalTask> updateResponse = elasticClient.Update<DalTask>(entity.Id, u => u.Doc(entity));
        }

        public void Delete(int id)
        {
            IDeleteResponse deleteResponse = elasticClient.Delete<DalTask>(id.ToString());
        }
        #endregion

        #region Get
        public IEnumerable<DalTask> GetAll()
        {
            ISearchResponse<DalTask> searchResponse = elasticClient.Search<DalTask>();
            return searchResponse.Documents;
        }

        public DalTask GetById(int id)
        {
            IGetResponse<DalTask> getResponse = elasticClient.Get<DalTask>(id.ToString());
            return getResponse.Source;
        }

        public IEnumerable<DalTask> GetQueryResults(string query)
        {
            ISearchResponse<DalTask> searchResponse = 
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

            return searchResponse.Documents;
        }
        #endregion
    }
}
