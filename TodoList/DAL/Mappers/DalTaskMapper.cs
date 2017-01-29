using System;
using DAL.Interfaces.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalTaskMapper
    {
        public static DalTask ToDalTaks(this Task ormTask)
        {
            if (ormTask == null)
                throw new ArgumentNullException(nameof(ormTask));

            return new DalTask
            {
                Id = ormTask.Id,
                Title = ormTask.Title,
                Description = ormTask.Description,
                PublishDate = ormTask.PublishDate,
                IsCompleted = ormTask.IsCompleted
            };
        }

        public static Task ToOrmTask(this DalTask dalTask)
        {
            if (dalTask == null)
                throw new ArgumentNullException(nameof(dalTask));

            return new Task
            {
                Id = dalTask.Id,
                Title = dalTask.Title,
                Description = dalTask.Description,
                PublishDate = dalTask.PublishDate,
                IsCompleted = dalTask.IsCompleted
            };
        }
    }
}
