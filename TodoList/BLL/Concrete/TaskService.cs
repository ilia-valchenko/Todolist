using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services.EntityService;
using DAL.Interfaces.Repository.ModelRepository;
using BLL.Mappers;
// test
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BLL.Concrete
{
    public class TaskService : ITaskService
    {
        public TaskService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
            // sets default setting 
            // port: 9200 etc.
            elasticClient = new ElasticLowLevelClient();
        }

        public void Create(BllTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException("The bll task entity is null.");

            taskRepository?.Create(entity.ToDalTask());

            try
            {
                elasticClient.Index<BllTask>("taskmanager", "tasks", JsonConvert.SerializeObject(entity));
            }
            catch(JsonSerializationException serializationException)
            {
                // write it into a logfile
                Debug.WriteLine($"Serialization Exception. Error message: {serializationException.Message}. StackTrace: {serializationException.StackTrace}");
            }
            catch(ElasticsearchClientException elasticClientException)
            {
                // write it into a logfile
                Debug.WriteLine($"Elasticsearch Client Exception. Error message: {elasticClientException.Message}. StackTrace: {elasticClientException.StackTrace}");
            }
            catch(Exception exc)
            {
                // write it into a logfile
                Debug.WriteLine($"Error. Message: {exc.Message}. StackTrace: {exc.StackTrace}.");
            }
        }

        public void Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("The Id of deleting task can't be less then zero.");

            taskRepository?.Delete(id);
        }

        public IEnumerable<BllTask> GetAll() => taskRepository?.GetAll().Select(t => t.ToBllTask());

        public BllTask GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("The Id of seeking task can't be less then zero.");

            return taskRepository?.GetById(id).ToBllTask();
        }

        public IEnumerable<BllTask> GetTasksByDate(DateTime date)
        {
            //if (date == null)
            //    throw new ArgumentNullException(nameof(date));

            throw new NotImplementedException();
        }

        public void Update(BllTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException("The bll task entity is null.");

            taskRepository?.Update(entity.ToDalTask());
        }

        private ITaskRepository taskRepository;
        private ElasticLowLevelClient elasticClient;
    }
}
