using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Interfaces.Repository;
using DAL.Interfaces.DTO;
using DAL.Concrete;

namespace RESTService.Controllers
{
    public class TaskController : ApiController
    {
        //public TaskController(TaskRepository taskRepository)
        //{
        //    this.taskRepository = taskRepository;
        //}

        [HttpGet]
        public IHttpActionResult ShowTodoList()
        {
            return Json(taskRepository?.GetAll());
        }

        //[HttpPost]
        //public IHttpActionResult AddNewItem(CreateTaskViewModel createTaskViewModel)
        //{
        //    DalTask dalTask = createTaskViewModel.ToDalTask();
        //    dalTask.PublishDate = DateTime.Now;
        //    // take it from cookies
        //    dalTask.AuthorId = 1;
        //    dalTask.AuthorNickname = "Batman";

        //    taskRepository?.Create(dalTask);

        //    return Json(dalTask.ToTaskViewModel());
        //}

        //private ITaskRepository taskRepository;
        private TaskRepository taskRepository = new TaskRepository();
    }
}
