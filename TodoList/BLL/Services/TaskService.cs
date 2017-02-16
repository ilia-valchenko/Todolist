using System;
using System.Collections.Generic;
using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using BLL.Models;
using Infrastructure.Mapper;

namespace BLL.Services
{
    public sealed class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly IElasticRepository elasticRepository;
        private readonly string indexName;

        public TaskService(ITaskRepository taskRepository, IElasticRepository elasticRepository, IMapper mapper, string indexName)
        {
            this.taskRepository = taskRepository;
            this.elasticRepository = elasticRepository;
            this.indexName = indexName;
        }

        #region CRUD
        public TaskModel Create(TaskModel task)
        {
            task.PublishDate = DateTime.Now;

            TaskEntity createdTaskEntity = Mapper.Map<TaskEntity>(task);

            createdTaskEntity.Id = taskRepository.Create(createdTaskEntity);
            elasticRepository.Create(createdTaskEntity, indexName);

            TaskModel createdTaskModel = Mapper.Map<TaskModel>(createdTaskEntity);

            return createdTaskModel;
        }

        public void Update(TaskModel task)
        {
            TaskEntity oldTaskEntity = taskRepository.GetById(task.Id);

            oldTaskEntity.Title = task.Title;
            oldTaskEntity.Description = task.Description;

            taskRepository.Update(oldTaskEntity);
            elasticRepository.Update(oldTaskEntity, indexName);
        }

        public void Delete(int id)
        {
            id = -1;

            if (id < 0)
            {
                throw new ArgumentException("The Id of deleting task can't be less then zero.");
            }

            TaskEntity task = taskRepository.GetById(id);

            taskRepository.Delete(task);
            elasticRepository.Delete(id, indexName);
        }
        #endregion

        #region Get operations
        public IEnumerable<TaskModel> GetAll()
        {
            IEnumerable<TaskEntity> TaskEntities = elasticRepository.GetAll(indexName);
            IEnumerable<TaskModel> TaskModels = Mapper.Map<IEnumerable<TaskModel>>(TaskEntities);
            return TaskModels;
        }

        //public IEnumerable<TaskModel> GetAll()
        //{
        //    IEnumerable<TaskEntity> TaskEntitys = taskRepository.GetAll();

        //    foreach (var item in TaskEntitys)
        //        elasticRepository.Create(item, indexName);

        //    IEnumerable<TaskModel> TaskModels = Mapper.Map<IEnumerable<TaskModel>>(TaskEntitys);

        //    return TaskModels;
        //}

        public TaskModel GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The Id of seeking task can't be less then zero.");
            }

            TaskEntity TaskEntity = taskRepository.GetById(id);
            TaskModel TaskModel = Mapper.Map<TaskModel>(TaskEntity);
            return TaskModel;
        }

        public IEnumerable<TaskModel> GetQueryResults(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                IEnumerable<TaskEntity> TaskEntitys = elasticRepository.GetAll(indexName);
                IEnumerable<TaskModel> TaskModels = Mapper.Map<IEnumerable<TaskModel>>(TaskEntitys);
                return TaskModels;
            }

            IEnumerable<TaskEntity> queryResultTaskEntitys = elasticRepository.GetQueryResults(query.ToLowerInvariant(), indexName);
            IEnumerable<TaskModel> queryResultTaskModels = Mapper.Map<IEnumerable<TaskModel>>(queryResultTaskEntitys);
            return queryResultTaskModels;
        } 
        #endregion
    }
}
