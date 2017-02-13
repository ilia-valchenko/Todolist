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
            mapper.CreateMap<BllTask, TaskViewModel>();
            mapper.CreateMap<TaskViewModel, BllTask>();
            mapper.CreateMap<DalTask, BllTask>();
            mapper.CreateMap<BllTask, DalTask>();
            mapper.CreateMap<CreateTaskViewModel, BllTask>();
            mapper.CreateMap<EditTaskViewModel, BllTask>();
        }
    }
}