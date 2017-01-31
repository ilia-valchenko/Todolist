using System;
using BLL.Interfaces.Entities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class BllTaskMapper
    {
        public static BllTask ToBllTask(this DalTask dalTask)
        {
            if (dalTask == null)
                throw new ArgumentNullException(nameof(dalTask));

            return new BllTask
            {
                Id = dalTask.Id,
                Title = dalTask.Title,
                Description = dalTask.Description,
                PublishDate = dalTask.PublishDate,
                IsCompleted = dalTask.IsCompleted
            };
        }

        public static DalTask ToDalTask(this BllTask bllTask)
        {
            if (bllTask == null)
                throw new ArgumentNullException(nameof(bllTask));

            return new DalTask
            {
                Id = bllTask.Id,
                Title = bllTask.Title,
                Description = bllTask.Description,
                PublishDate = bllTask.PublishDate,
                IsCompleted = bllTask.IsCompleted
            };
        }
    }
}
