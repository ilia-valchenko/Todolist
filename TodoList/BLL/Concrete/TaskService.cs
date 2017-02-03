using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services.EntityService;
using DAL.Interfaces.Repository.ModelRepository;
using BLL.Mappers;
using DAL.Interfaces.DTO;

namespace BLL.Concrete
{
    public class TaskService : ITaskService
    {
        public TaskService(ITaskRepository taskRepository, IElasticRepository elasticRepository)
        {
            this.taskRepository = taskRepository;
            this.elasticRepository = elasticRepository;
        }

        #region CRUD
        public void Create(BllTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException("The bll task entity is null.");

            DalTask task = entity.ToDalTask();

            taskRepository?.Create(task);
            elasticRepository?.Create(task);
        }

        public void Update(BllTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException("The bll task entity is null.");

            taskRepository?.Update(entity.ToDalTask());
            elasticRepository?.Update(entity.ToDalTask());
        }

        public void Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("The Id of deleting task can't be less then zero.");

            taskRepository?.Delete(id);
            elasticRepository?.Delete(id);
        }
        #endregion

        //public IEnumerable<BllTask> GetAll() => taskRepository?.GetAll().Select(t => t.ToBllTask());

        public IEnumerable<BllTask> GetAll() => elasticRepository?.GetAll().Select(t => t.ToBllTask());

        //public IEnumerable<BllTask> GetAll()
        //{
        //    foreach (var item in taskRepository?.GetAll())
        //        elasticRepository.Create(item);

        //    return taskRepository?.GetAll().Select(t => t.ToBllTask());
        //}

        public BllTask GetById(int id)
        {
            if (id < 0)
                throw new ArgumentException("The Id of seeking task can't be less then zero.");

            return elasticRepository?.GetById(id).ToBllTask();
        }

        public IEnumerable<BllTask> GetTasksByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BllTask> GetQueryResults(string query)
        {
            if (String.IsNullOrEmpty(query))
                return elasticRepository?.GetAll().Select(t => t.ToBllTask());

            return elasticRepository?.GetQueryResults(query).Select(t => t.ToBllTask());
        }

        private ITaskRepository taskRepository;
        private IElasticRepository elasticRepository;
    }
}
