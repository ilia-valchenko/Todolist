using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces.DTO;
using NHibernate;
using DAL.Helpers;
using DAL.Mappers;
using ORM.Models;
using DAL.Interfaces.Repository.ModelRepository;
using Nest;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DAL.Concrete
{
    public class ElasticRepository : IElasticRepository
    {
        public ElasticRepository()
        {
            elasticClient = new ElasticLowLevelClient();
        }

        #region CRUD
        public void Create(DalTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                elasticClient.Index<DalTask>("taskmanager", "tasks", JsonConvert.SerializeObject(entity));
            }
            catch (JsonSerializationException serializationException)
            {
                // write it into a logfile
                Debug.WriteLine($"Serialization Exception. Error message: {serializationException.Message}. StackTrace: {serializationException.StackTrace}");
            }
            catch (ElasticsearchClientException elasticClientException)
            {
                // write it into a logfile
                Debug.WriteLine($"Elasticsearch Client Exception. Error message: {elasticClientException.Message}. StackTrace: {elasticClientException.StackTrace}");
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

            DalTask res = elasticClient.Search<DalTask>(new DalTask { Title = query }).Body;

            // stub
            return null;
        }
        #endregion

        private ElasticLowLevelClient elasticClient;
    }
}
