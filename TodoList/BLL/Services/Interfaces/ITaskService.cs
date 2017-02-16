using System.Collections.Generic;
using BLL.Models;

namespace BLL.Services.Interfaces
{
    public interface ITaskService
    {
        TaskModel Create(TaskModel task);
        void Update(TaskModel task);
        void Delete(int id);
        TaskModel GetById(int id);
        IEnumerable<TaskModel> GetAll();
        IEnumerable<TaskModel> GetQueryResults(string query);
    }
}
