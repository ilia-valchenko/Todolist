using System;
using System.Collections.Generic;
using System.Linq;
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
            if (createdBllTask == null)
            {
                throw new ArgumentNullException("The bll task entity is null.");
            }
                
            createdBllTask.PublishDate = DateTime.Now;
            createdBllTask.IsCompleted = false;

            DalTask createdDalTask;

            try
            {
                createdDalTask = Mapper.Map<DalTask>(createdBllTask);
            }
            catch(AutoMapperMappingException mappingException)
            {
                // ILogger
                // throw new
                throw;
            }

            taskRepository.Create(createdDalTask);
            elasticRepository.Create(createdDalTask);
        }

        public void Update(BllTask updatedBllTask)
        {
            if (updatedBllTask == null)
            {
                throw new ArgumentNullException("The bll task entity is null. You cannot update this entity.");
            }

            DalTask updatedDalTask;
                
            try
            {
                updatedDalTask = Mapper.Map<DalTask>(updatedBllTask);
            }
            catch(AutoMapperMappingException mappingException)
            {
                // ILogger
                throw;
            }
             
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
            IEnumerable<BllTask> bllTasks;

            try
            {
                bllTasks = Mapper.Map<IEnumerable<BllTask>>(dalTasks);
            }    
            catch(AutoMapperMappingException mappingException)
            {
                // ILogger
                throw;
            }    

            return bllTasks;
        }

        //public IEnumerable<BllTask> GetAll()
        //{
        //    foreach (var item in taskRepository?.GetAll())
        //        elasticRepository.Create(item);

        //    return taskRepository?.GetAll().Select(t => t.ToBllTask());
        //}

        public BllTask GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The Id of seeking task can't be less then zero.");
            }

            DalTask dalTask = elasticRepository.GetById(id);
            BllTask bllTask;

            try
            {
                bllTask = Mapper.Map<BllTask>(dalTask);
            }
            catch(AutoMapperMappingException mappingException)
            {
                // ILogger
                throw;
            }

            return bllTask;
        }

        public IEnumerable<BllTask> GetQueryResults(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                IEnumerable<DalTask> dalTasks = elasticRepository.GetAll();
                IEnumerable<BllTask> bllTasks;

                try
                {
                    bllTasks = Mapper.Map<IEnumerable<BllTask>>(dalTasks);
                }
                catch(AutoMapperMappingException mappingException)
                {
                    // ILogger
                    throw;
                }

                return bllTasks;
            }

            IEnumerable<DalTask> queryResultDalTasks = elasticRepository.GetQueryResults(query.ToLowerInvariant());
            IEnumerable<BllTask> queryResultBllTasks;

            try
            {
                queryResultBllTasks = Mapper.Map<IEnumerable<BllTask>>(queryResultDalTasks);
            }
            catch(AutoMapperMappingException mappingException)
            {
                // ILogger
                throw;
            }

            return queryResultBllTasks;
        } 
        #endregion
    }
}
