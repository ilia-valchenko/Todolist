using System;
using System.Collections.Generic;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository.ModelRepository;
using Nest;
using System.Diagnostics;

namespace DAL.Concrete
{
    public class ElasticRepository : IElasticRepository
    {
        public ElasticRepository(/*IElasticClient elasticClient*/)
        {
            /*this.elasticClient = elasticClient;*/

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
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                elasticClient.Index(entity);
            }
            catch (Exception exc)
            {
                // write it into a logfile
                Debug.WriteLine($"Error. Message: {exc.Message}. StackTrace: {exc.StackTrace}.");
            }
        }

        public void Update(DalTask entity)
        {
            elasticClient?.Update<DalTask>(entity.Id, u => u.Doc(entity));
        }

        public void Delete(int id)
        {
            elasticClient?.Delete<DalTask>(id.ToString());
        }
        #endregion

        #region Get
        public IEnumerable<DalTask> GetAll() => elasticClient?.Search<DalTask>().Documents;

        public DalTask GetById(int id) => elasticClient?.Get<DalTask>(id.ToString()).Source;

        public IEnumerable<DalTask> GetQueryResults(string query)
        {
            return elasticClient.Search<DalTask>(s => s
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
            ).Documents;
        }
        #endregion

        private IElasticClient elasticClient;
    }
}
