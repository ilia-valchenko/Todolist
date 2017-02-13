using System;
using System.Collections.Generic;
using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using BLL.Models;
using AutoMapper;

namespace BLL.Services.Concrete
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly IElasticRepository elasticRepository;
        private readonly string indexName;

        public TaskService(ITaskRepository taskRepository, IElasticRepository elasticRepository, string indexName)
        {
            this.taskRepository = taskRepository;
            this.elasticRepository = elasticRepository;
            this.indexName = indexName;
        }

        #region CRUD
        public BllTask Create(BllTask task)
        {
            task.PublishDate = DateTime.Now;

            DalTask createdDalTask = Mapper.Map<DalTask>(task);

            createdDalTask.Id = taskRepository.Create(createdDalTask);
            elasticRepository.Create(createdDalTask, indexName);

            BllTask createdBllTask = Mapper.Map<BllTask>(createdDalTask);

            return createdBllTask;
        }

        public void Update(BllTask updatedBllTask)
        {
            DalTask updatedDalTask = Mapper.Map<DalTask>(updatedBllTask);
            taskRepository.Update(updatedDalTask);
            elasticRepository.Update(updatedDalTask, indexName);
        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The Id of deleting task can't be less then zero.");
            }

            taskRepository.Delete(id);
            elasticRepository.Delete(id, indexName);
        }
        #endregion

        #region Get operations
        public IEnumerable<BllTask> GetAll()
        {
            IEnumerable<DalTask> dalTasks = elasticRepository.GetAll(indexName);
            IEnumerable<BllTask> bllTasks = Mapper.Map<IEnumerable<BllTask>>(dalTasks);
            return bllTasks;
        }

        //public IEnumerable<BllTask> GetAll()
        //{
        //    IEnumerable<DalTask> dalTasks = taskRepository.GetAll();

        //    foreach (var item in dalTasks)
        //        elasticRepository.Create(item, indexName);

        //    IEnumerable<BllTask> bllTasks = Mapper.Map<IEnumerable<BllTask>>(dalTasks);

        //    return bllTasks;
        //}

        public BllTask GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The Id of seeking task can't be less then zero.");
            }

            DalTask dalTask = taskRepository.GetById(id);
            BllTask bllTask = Mapper.Map<BllTask>(dalTask);
            return bllTask;
        }

        public IEnumerable<BllTask> GetQueryResults(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                IEnumerable<DalTask> dalTasks = elasticRepository.GetAll(indexName);
                IEnumerable<BllTask> bllTasks = Mapper.Map<IEnumerable<BllTask>>(dalTasks);
                return bllTasks;
            }

            IEnumerable<DalTask> queryResultDalTasks = elasticRepository.GetQueryResults(query.ToLowerInvariant(), indexName);
            IEnumerable<BllTask> queryResultBllTasks = Mapper.Map<IEnumerable<BllTask>>(queryResultDalTasks);
            return queryResultBllTasks;
        } 
        #endregion
    }
}
