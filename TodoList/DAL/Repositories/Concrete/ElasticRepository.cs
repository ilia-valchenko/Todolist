using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Nest;
using System.Linq;

namespace DAL.Repositories.Concrete
{
    public sealed class ElasticRepository : IElasticRepository
    {
        private readonly IElasticClient elasticClient;

        public ElasticRepository(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        #region CRUD
        public void Create(DalTask entity, string indexName)
        {
            IIndexResponse indexResponse = elasticClient.Index(entity, i => i.Index(indexName));
        }

        public void Update(DalTask entity, string indexName)
        {
            IUpdateResponse<DalTask> updateResponse = elasticClient.Update<DalTask>(entity.Id, i => i.Index(indexName).Doc(entity));
        }

        public void Delete(int id, string indexName)
        {
            IDeleteResponse deleteResponse = elasticClient.Delete<DalTask>(id, i => i.Index(indexName));
        }
        #endregion

        #region Get
        public IEnumerable<DalTask> GetAll(string indexName)
        {
            ISearchResponse<DalTask> searchResponse = elasticClient.Search<DalTask>(i => i.Index(indexName));
            return searchResponse.Documents;
        }

        public DalTask GetById(int id, string indexName)
        {
            IGetResponse<DalTask> getResponse = elasticClient.Get<DalTask>(id, i => i.Index(indexName));
            return getResponse.Source;
        }

        public IEnumerable<DalTask> GetQueryResults(string query, string indexName)
        {
            ISearchResponse<DalTask> searchResponse =
                elasticClient.Search<DalTask>(s => s
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
            IEnumerable<DalTask> searchResults = searchResponse.Documents;
            IReadOnlyCollection<IHit<DalTask>> hits = searchResponse.Hits;
            
            for(int i = 0; i < hits.Count; i++)
            {
                var highlights = hits.ElementAt(i).Highlights;
                var highlightedTextTitle = highlights["Title"].Highlights.ElementAt(0);
                searchResults.ElementAt(i).Title = highlightedTextTitle;
            }

            return searchResults;
        }
        #endregion
    }
}
