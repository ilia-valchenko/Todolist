using System.Web.Http;
using DAL.Interfaces.Repository.ModelRepository;

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

        private ITaskRepository taskRepository;
    }
}
