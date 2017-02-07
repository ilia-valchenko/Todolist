using System.Web.Http;
using BLL.Services.Interfaces;
using System.Collections.Generic;
using RESTService.ViewModels;
using AutoMapper;
using BLL.Models;

namespace RESTService.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public IHttpActionResult ShowTodoList()
        {
            IEnumerable<BllTask> bllTasks = taskService.GetAll();
            IEnumerable<TaskViewModel> viewModelTasks = Mapper.Map<IEnumerable<TaskViewModel>>(bllTasks);
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
        public IHttpActionResult AddNewTask(CreateTaskViewModel createTaskViewModel)
        {
            if(ModelState.IsValid)
            {
                BllTask task = Mapper.Map<BllTask>(createTaskViewModel);
                BllTask commitedBllTask = taskService.Create(task);
                TaskViewModel commitedViewModelTask = Mapper.Map<TaskViewModel>(commitedBllTask);
                var jsonCommitedTaskViewModel = Json(commitedViewModelTask);
                return jsonCommitedTaskViewModel;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IHttpActionResult Update(EditTaskViewModel editTaskViewModel)
        {
            if(ModelState.IsValid)
            {
                BllTask task = Mapper.Map<BllTask>(editTaskViewModel);
                taskService.Update(task);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            taskService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [ActionName("search")]
        public IHttpActionResult GetQueryResults(string query)
        {
            IEnumerable<BllTask> bllTasks = taskService.GetQueryResults(query);
            IEnumerable<TaskViewModel> viewModelTasks = Mapper.Map<IEnumerable<TaskViewModel>>(bllTasks);
            var jsonQueryResults = Json(viewModelTasks);
            return jsonQueryResults;
        }  
    }
}
