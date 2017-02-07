using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IElasticRepository
    {
        void Create(DalTask task);
        void Update(DalTask task);
        void Delete(int id);
        DalTask GetById(int id);
        IEnumerable<DalTask> GetAll();
        IEnumerable<DalTask> GetQueryResults(string query);
    }
}
