using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Nest;
using System.Linq;

namespace DAL.Repositories.ElasticRepositories
{
    public sealed class ElasticRepository : IElasticRepository
    {
        private readonly IElasticClient elasticClient;
        private const string nameOfHighlightedField = "Title";

        public ElasticRepository(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        #region CRUD
        public void Create(TaskEntity entity, string indexName)
        {
            IIndexResponse indexResponse = elasticClient.Index(entity, i => i.Index(indexName));
        }

        public void Update(TaskEntity entity, string indexName)
        {
            IUpdateResponse<TaskEntity> updateResponse = elasticClient.Update<TaskEntity>(entity.Id, i => i.Index(indexName).Doc(entity));
        }

        public void Delete(int id, string indexName)
        {
            IDeleteResponse deleteResponse = elasticClient.Delete<TaskEntity>(id, i => i.Index(indexName));
        }
        #endregion

        #region Get operations
        public IEnumerable<TaskEntity> GetAll(string indexName)
        {
            ISearchResponse<TaskEntity> searchResponse = elasticClient.Search<TaskEntity>(i => i.Index(indexName));
            return searchResponse.Documents;
        }

        public TaskEntity GetById(int id, string indexName)
        {
            IGetResponse<TaskEntity> getResponse = elasticClient.Get<TaskEntity>(id, i => i.Index(indexName));
            return getResponse.Source;
        }

        public IEnumerable<TaskEntity> GetQueryResults(string query, string indexName)
        {
            ISearchResponse<TaskEntity> searchResponse =
                elasticClient.Search<TaskEntity>(s => s
                .Index(indexName)
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Title)
                            .Query(query)
                        )
                    )
                    .Highlight(h => h
                        .Fields(fs => fs
                            .Field(fl => fl.Title)
                        )
                    )
                );

            // Add new entity for displaying tasks with highlights
            IEnumerable<TaskEntity> searchResults = searchResponse.Documents;
            IReadOnlyCollection<IHit<TaskEntity>> hits = searchResponse.Hits;
            
            for(int i = 0; i < hits.Count; i++)
            {
                var highlights = hits.ElementAt(i).Highlights;
                var highlightedTextTitle = highlights[nameOfHighlightedField].Highlights.ElementAt(0);
                searchResults.ElementAt(i).Title = highlightedTextTitle;
            }

            return searchResults;
        }
        #endregion
    }
}
