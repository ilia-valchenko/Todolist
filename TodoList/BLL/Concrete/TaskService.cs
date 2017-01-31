using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services.EntityService;
using DAL.Interfaces.Repository.ModelRepository;
using BLL.Mappers;

namespace BLL.Concrete
{
    public class TaskService : ITaskService
    {
        public TaskService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public void Create(BllTask entity)
        {
            if (entity == null)
                throw new ArgumentNullException("The bll task entity is null.");

            taskRepository?.Create(entity.ToDalTask());
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
    }
}
