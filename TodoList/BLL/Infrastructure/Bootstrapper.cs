using Microsoft.Practices.Unity;
using DAL.Repositories.Interfaces;
using DAL.Repositories.ElasticRepositories;
using DAL.Repositories.DatabaseRepositories;
using BLL.Models;
using DAL.Entities;
using AutoMapper;

namespace BLL.Infrastructure
{
    public class Bootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            DAL.Infrastructure.Bootstrapper.Register(container);

            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IElasticRepository, ElasticRepository>(new HierarchicalLifetimeManager());
        }

        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<TaskEntity, TaskModel>().ReverseMap();
            });
        }
    }
}
