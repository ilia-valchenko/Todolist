using System;
using System.Web.Http;
using DAL.Interfaces.Repository.ModelRepository;
using DAL.Interfaces.DTO;

namespace RESTService.Controllers
{
    public class TaskController : ApiController
    {
        public TaskController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        [HttpGet]
        public IHttpActionResult ShowTodoList()
        {
            return Json(taskRepository?.GetAll());
        }

        [HttpPost]
        public IHttpActionResult AddNewTask(DalTask task)
        {
            task.PublishDate = DateTime.Now;
            task.IsCompleted = false;

            taskRepository?.Create(task);
            return Json(task);
        }

        private ITaskRepository taskRepository;
    }
}
