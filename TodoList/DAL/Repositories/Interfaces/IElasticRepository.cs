using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IElasticRepository : IRepository<DalTask>
    {
        IEnumerable<DalTask> GetQueryResults(string query);
    }
}
