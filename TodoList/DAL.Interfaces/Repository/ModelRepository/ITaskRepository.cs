using System;
using System.Collections.Generic;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository.ModelRepository
{
    public interface ITaskRepository : IRepository<DalTask>
    {
        IEnumerable<DalTask> GetTasksByDate(DateTime date);
    }
}
