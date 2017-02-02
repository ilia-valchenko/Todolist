using System;
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
        public IHttpActionResult ShowTodoList()
        {
            return Json(taskService?.GetAll());
        }

        [HttpGet]
        public IHttpActionResult Details(int id) => Json(taskService?.GetById(id));

        [HttpPost]
        public IHttpActionResult AddNewTask(BllTask task)
        {
            task.PublishDate = DateTime.Now;
            task.IsCompleted = false;

            taskService?.Create(task);
            return Json(task);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                taskService?.Delete(id);
            }
            catch(Exception exc)
            {
                // change type of exception
                // handle it 
                // log it
                return BadRequest();
            }

            return Ok();
        } 

        [HttpGet]
        [ActionName("search")]
        public IHttpActionResult GetQueryResults(string query)
        {
            return Json(taskService?.GetQueryResults(query));
        }

        private ITaskService taskService;
    }
}
