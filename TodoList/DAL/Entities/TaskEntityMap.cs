using FluentNHibernate.Mapping;

namespace DAL.Entities
{
    public sealed class TaskEntityMap : ClassMap<TaskEntity>
    {
        public TaskEntityMap()
        {
            Table("Task");
            Id(x => x.Id).CustomType<int>();
            Map(x => x.Title);
            Map(x => x.Description);
            Map(x => x.PublishDate);
            Map(x => x.IsCompleted);
        }
    }
}
