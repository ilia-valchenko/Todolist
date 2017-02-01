using FluentNHibernate.Mapping;

namespace ORM.Models
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Description);
            Map(x => x.PublishDate);
            Map(x => x.IsCompleted);
        }
    }
}
