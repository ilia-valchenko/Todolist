using System.Collections.Generic;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository.ModelRepository
{
    public interface IElasticRepository : IRepository<DalTask>
    {
        IEnumerable<DalTask> GetQueryResults(string query);
    }
}
