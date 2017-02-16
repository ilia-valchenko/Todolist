using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IElasticRepository
    {
        void Create(TaskEntity task, string indexName);
        void Update(TaskEntity task, string indexName);
        void Delete(int id, string indexName);
        TaskEntity GetById(int id, string indexName);
        IEnumerable<TaskEntity> GetAll(string indexName);
        IEnumerable<TaskEntity> GetQueryResults(string query, string indexName);
    }
}
