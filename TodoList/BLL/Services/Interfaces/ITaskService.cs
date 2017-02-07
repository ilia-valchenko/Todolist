using System.Collections.Generic;
using BLL.Models;

namespace BLL.Services.Interfaces
{
    public interface ITaskService
    {
        BllTask Create(BllTask task);
        void Update(BllTask task);
        void Delete(int id);
        BllTask GetById(int id);
        IEnumerable<BllTask> GetAll();
        IEnumerable<BllTask> GetQueryResults(string query);
    }
}
