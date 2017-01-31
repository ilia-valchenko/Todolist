﻿using System;
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

        [HttpPost]
        public IHttpActionResult AddNewTask(BllTask task)
        {
            task.PublishDate = DateTime.Now;
            task.IsCompleted = false;

            taskService?.Create(task);
            return Json(task);
        }

        private ITaskService taskService;
    }
}
