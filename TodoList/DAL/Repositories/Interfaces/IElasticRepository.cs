using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IElasticRepository
    {
        void Create(DalTask task, string indexName);
        void Update(DalTask task, string indexName);
        void Delete(int id, string indexName);
        DalTask GetById(int id, string indexName);
        IEnumerable<DalTask> GetAll(string indexName);
        IEnumerable<DalTask> GetQueryResults(string query, string indexName);
    }
}
