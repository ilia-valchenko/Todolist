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
            IEnumerable<TaskModel> TaskModels = taskService.GetAll();
            IEnumerable<TaskViewModel> viewModelTasks = Mapper.Map<IEnumerable<TaskViewModel>>(TaskModels);
            var jsonTasks = Json(viewModelTasks);
            return jsonTasks;
        }

        [HttpGet]
        public IHttpActionResult Details(int id)
        {
            TaskModel detailedTaskModel = taskService.GetById(id);
            TaskViewModel detailedTaskViewModel = Mapper.Map<TaskViewModel>(detailedTaskModel);
            var detailedJsonTask = Json(detailedTaskViewModel);
            return detailedJsonTask;
        }

        [HttpPost]
        public IHttpActionResult AddNewTask(CreateTaskViewModel createTaskViewModel)
        {
            if(ModelState.IsValid)
            {
                TaskModel task = Mapper.Map<TaskModel>(createTaskViewModel);
                TaskModel commitedTaskModel = taskService.Create(task);
                TaskViewModel commitedViewModelTask = Mapper.Map<TaskViewModel>(commitedTaskModel);
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
                TaskModel task = Mapper.Map<TaskModel>(editTaskViewModel);
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
            IEnumerable<TaskModel> TaskModels = taskService.GetQueryResults(query);
            IEnumerable<TaskViewModel> viewModelTasks = Mapper.Map<IEnumerable<TaskViewModel>>(TaskModels);
            var jsonQueryResults = Json(viewModelTasks);
            return jsonQueryResults;
        }  
    }
}
