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
            elasticClient = new ElasticClient();
        }

        #region CRUD
        public void Create(DalTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                elasticClient.Index(entity, t => t.Index("taskmanager").Type("tasks"));
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
            if (String.IsNullOrEmpty(query))
                return null;
            
            // stub
            return null;
        }
        #endregion

        private IElasticClient elasticClient;
    }
}
