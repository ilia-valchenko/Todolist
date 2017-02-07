using System;
using System.Collections.Generic;
using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using BLL.Models;
using AutoMapper;
using BLL.Infrastructure;

namespace BLL.Services.Concrete
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly IElasticRepository elasticRepository;
        private readonly IIdentifierGenerator idGenerator;
        private int id;

        public TaskService(ITaskRepository taskRepository, IElasticRepository elasticRepository, IIdentifierGenerator idGenerator, int startId)
        {
            this.taskRepository = taskRepository;
            this.elasticRepository = elasticRepository;
            this.idGenerator = idGenerator;
            this.id = startId;
        }

        #region CRUD
        public BllTask Create(BllTask createdBllTask)
        {
            id = idGenerator.GenerateNextId(id);
            createdBllTask.Id = id;
            createdBllTask.PublishDate = DateTime.Now;

            DalTask createdDalTask = Mapper.Map<DalTask>(createdBllTask);

            DalTask commitedDalTask = taskRepository.Create(createdDalTask);
            elasticRepository.Create(createdDalTask);

            BllTask commitedBllTask = Mapper.Map<BllTask>(commitedDalTask);

            return commitedBllTask;
        }

        public void Update(BllTask updatedBllTask)
        {
            DalTask updatedDalTask = Mapper.Map<DalTask>(updatedBllTask);
             
            taskRepository.Update(updatedDalTask);
            elasticRepository.Update(updatedDalTask);
        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The Id of deleting task can't be less then zero.");
            }

            taskRepository.Delete(id);
            elasticRepository.Delete(id);
        }
        #endregion

        #region Get operations
        public IEnumerable<BllTask> GetAll()
        {
            IEnumerable<DalTask> dalTasks = elasticRepository.GetAll();
            IEnumerable<BllTask> bllTasks = Mapper.Map<IEnumerable<BllTask>>(dalTasks);
            return bllTasks;
        }

        //public IEnumerable<BllTask> GetAll()
        //{
        //    foreach (var item in taskRepository?.GetAll())
        //        elasticRepository.Create(item);

        //    IEnumerable<DalTask> dalTasks = taskRepository.GetAll();
        //    IEnumerable<BllTask> bllTasks = Mapper.Map<IEnumerable<BllTask>>(dalTasks);

        //    return bllTasks;
        //}

        public BllTask GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The Id of seeking task can't be less then zero.");
            }

            DalTask dalTask = elasticRepository.GetById(id);
            BllTask bllTask = Mapper.Map<BllTask>(dalTask);
            return bllTask;
        }

        public IEnumerable<BllTask> GetQueryResults(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                IEnumerable<DalTask> dalTasks = elasticRepository.GetAll();
                IEnumerable<BllTask> bllTasks = Mapper.Map<IEnumerable<BllTask>>(dalTasks);
                return bllTasks;
            }

            IEnumerable<DalTask> queryResultDalTasks = elasticRepository.GetQueryResults(query.ToLowerInvariant());
            IEnumerable<BllTask> queryResultBllTasks = Mapper.Map<IEnumerable<BllTask>>(queryResultDalTasks);
            return queryResultBllTasks;
        } 
        #endregion
    }
}
