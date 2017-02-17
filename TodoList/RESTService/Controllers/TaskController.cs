using System.Web.Http;
using BLL.Services.Interfaces;
using System.Collections.Generic;
using RESTService.ViewModels;
using BLL.Models;
using Common.Mapper;

namespace RESTService.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskService taskService;
        private readonly IMapper mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            this.taskService = taskService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IHttpActionResult ShowTodoList()
        {
            IEnumerable<TaskModel> taskModels = taskService.GetAll();
            IEnumerable<TaskViewModel> viewModelTasks = mapper.Map<IEnumerable<TaskModel>, IEnumerable<TaskViewModel>>(taskModels);
            var jsonTasks = Json(viewModelTasks);
            return jsonTasks;
        }

        [HttpGet]
        public IHttpActionResult Details(int id)
        {
            TaskModel detailedTaskModel = taskService.GetById(id);
            TaskViewModel detailedTaskViewModel = mapper.Map<TaskModel, TaskViewModel>(detailedTaskModel);
            var detailedJsonTask = Json(detailedTaskViewModel);
            return detailedJsonTask;
        }

        [HttpPost]
        public IHttpActionResult AddNewTask(CreateTaskViewModel createTaskViewModel)
        {
            if(ModelState.IsValid)
            {
                TaskModel task = mapper.Map<CreateTaskViewModel, TaskModel>(createTaskViewModel);
                TaskModel commitedTaskModel = taskService.Create(task);
                TaskViewModel commitedViewModelTask = mapper.Map<TaskModel, TaskViewModel>(commitedTaskModel);
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
                TaskModel task = mapper.Map<EditTaskViewModel, TaskModel>(editTaskViewModel);
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
            IEnumerable<TaskModel> taskModels = taskService.GetQueryResults(query);
            IEnumerable<TaskViewModel> viewModelTasks = mapper.Map<IEnumerable<TaskModel>, IEnumerable<TaskViewModel>>(taskModels);
            var jsonQueryResults = Json(viewModelTasks);
            return jsonQueryResults;
        }  
    }
}
