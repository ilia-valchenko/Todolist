using System.Web.Http;
using BLL.Services.Concrete;
using BLL.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;
using RESTService.ViewModels;
using AutoMapper;
using BLL.Models;
using AutoMapper.Mappers;
using System.Web.Http.Results;

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
            IEnumerable<BllTask> bllTasks = taskService.GetAll();
            IEnumerable<TaskViewModel> viewModelTasks = bllTasks.Select(t => Mapper.Map<TaskViewModel>(t));
            var jsonTasks = Json(viewModelTasks);
            return jsonTasks;
        }

        [HttpGet]
        public IHttpActionResult Details(int id)
        {
            BllTask detailedBllTask = taskService.GetById(id);
            TaskViewModel detailedTaskViewModel = Mapper.Map<TaskViewModel>(detailedBllTask);
            var detailedJsonTask = Json(detailedTaskViewModel);
            return detailedJsonTask;
        }

        [HttpPost]
        public IHttpActionResult AddNewTask(BllTask task)
        {
            taskService.Create(task);
            // check for null value
            // bad request
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(BllTask task)
        {
            taskService.Update(task);
            // check for null value
            // bad request
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            taskService.Delete(id);
            // check for
            // bad request
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
