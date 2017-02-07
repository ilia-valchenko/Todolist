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

        public TaskService(ITaskRepository taskRepository, IElasticRepository elasticRepository)
        {
            this.taskRepository = taskRepository;
            this.elasticRepository = elasticRepository;
        }

        #region CRUD
        public void Create(BllTask createdBllTask)
        {               
            createdBllTask.PublishDate = DateTime.Now;

            DalTask createdDalTask = Mapper.Map<DalTask>(createdBllTask);

            taskRepository.Create(createdDalTask);
            elasticRepository.Create(createdDalTask);
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
