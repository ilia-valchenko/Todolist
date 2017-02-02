﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Concrete;
using DAL.Concrete;
using DAL.Interfaces;
using BLL.Interfaces.Entities;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskService service = new TaskService(new TaskRepository(), new ElasticRepository());

            ElasticRepository repository = new ElasticRepository();

            var res = repository.GetById(2);

            //foreach (var task in repository.GetQueryResults("world"))
            //    Console.WriteLine($"Id: {task.Id}, Title: {task.Title}, Description: {task.Description}\n\n");

            Console.WriteLine("Tap to continue...");
            Console.ReadKey(true);
        }
    }
}
