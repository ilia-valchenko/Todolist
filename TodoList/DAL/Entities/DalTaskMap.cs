using FluentNHibernate.Mapping;

namespace DAL.Entities
{
    public class DalTaskMap : ClassMap<DalTask>
    {
        public DalTaskMap()
        {           
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Description);
            Map(x => x.PublishDate);
            Map(x => x.IsCompleted);
        }
    }
}
