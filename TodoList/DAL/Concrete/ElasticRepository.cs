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
            //this.elasticClient = elasticClient;

            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("taskmanager").DefaultTypeNameInferrer(t => "tasks");
            elasticClient = new ElasticClient(settings);
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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Get
        public IEnumerable<DalTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalTask GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalTask> GetQueryResults(string query)
        {
            var res = elasticClient.Search<DalTask>(s => s.Type("tasks").Query(q => q.Match(m => m.Field(f => f.Title).Query("serpico"))));
            // stub
            return null;
        }
        #endregion

        public IEnumerable<DalTask> NewGetById(int id)
        {
            return elasticClient.Search<DalTask>(s => s.Type("tasks")
            .Query(q => q
                .Bool(b => b
                    .Should(
                        bs => bs.Term(p => p.Id, id)
                    )
                )
            )
            ).Documents;
        }

        private IElasticClient elasticClient;
    }
}
