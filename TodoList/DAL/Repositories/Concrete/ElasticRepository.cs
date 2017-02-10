using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Nest;
using System.Linq;

namespace DAL.Repositories.Concrete
{
    public class ElasticRepository : IElasticRepository
    {
        private readonly IElasticClient elasticClient;

        public ElasticRepository(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        #region CRUD
        public void Create(DalTask entity)
        {
            IIndexResponse indexResponse = elasticClient.Index(entity, i => i.Index("taskmanager"));
        }

        public void Update(DalTask entity)
        {
            IUpdateResponse<DalTask> updateResponse = elasticClient.Update<DalTask>(entity.Id, i => i.Index("taskmanager").Doc(entity));
        }

        public void Delete(int id)
        {
            IDeleteResponse deleteResponse = elasticClient.Delete<DalTask>(id.ToString(), i => i.Index("taskmanager"));
        }
        #endregion

        #region Get
        public IEnumerable<DalTask> GetAll()
        {
            ISearchResponse<DalTask> searchResponse = elasticClient.Search<DalTask>(i => i.Index("taskmanager"));
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
                .Index("taskmanager")
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
