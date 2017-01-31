using System;
using System.Collections.Generic;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services.EntityService
{
    public interface ITaskService : IService<BllTask>
    {
        IEnumerable<BllTask> GetTasksByDate(DateTime date);
    }
}
