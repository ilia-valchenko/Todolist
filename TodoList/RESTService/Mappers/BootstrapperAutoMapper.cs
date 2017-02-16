using AutoMapper;
using Bootstrap.AutoMapper;
using BLL.Models;
using RESTService.ViewModels;
using DAL.Entities;

namespace RESTService.Mappers
{
    public sealed class BootstrapperAutoMapper : IMapCreator
    {
        public void CreateMap(IProfileExpression mapper)
        {
            mapper.CreateMap<TaskModel, TaskViewModel>().ReverseMap();
            //mapper.CreateMap<TaskViewModel, TaskModel>();
            mapper.CreateMap<TaskEntity, TaskModel>();
            mapper.CreateMap<TaskModel, TaskEntity>();
            mapper.CreateMap<CreateTaskViewModel, TaskModel>();
            mapper.CreateMap<EditTaskViewModel, TaskModel>();
        }
    }
}