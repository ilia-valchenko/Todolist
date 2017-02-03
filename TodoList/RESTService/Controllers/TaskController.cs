using System.Web.Http;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services.EntityService;

namespace RESTService.Controllers
{
    public class TaskController : ApiController
    {
        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public IHttpActionResult ShowTodoList() => Json(taskService?.GetAll());

        [HttpGet]
        public IHttpActionResult Details(int id) => Json(taskService?.GetById(id));

        [HttpPost]
        public IHttpActionResult AddNewTask(BllTask task)
        {
            taskService?.Create(task);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(BllTask task)
        {
            taskService?.Update(task);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            taskService?.Delete(id);
            return Ok();
        } 

        [HttpGet]
        [ActionName("search")]
        public IHttpActionResult GetQueryResults(string query) => Json(taskService?.GetQueryResults(query));

        private ITaskService taskService;
    }
}
