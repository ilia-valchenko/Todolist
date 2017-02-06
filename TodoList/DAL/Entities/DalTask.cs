using System;
using Nest;

namespace DAL.Entities
{
    [ElasticsearchType(IdProperty = "Id", Name = "task")]
    public class DalTask 
    {
        [Number(Name = "Id")]
        public virtual int Id { get; set; }
        [Text(Name = "Title", Index = false, Store = true, Analyzer = "mynGram")]
        public virtual string Title { get; set; }
        [Text(Name = "Description")]
        public virtual string Description { get; set; }
        [Date(Format = "MMddyyyy", Name = "PublishDate")]
        public virtual DateTime PublishDate { get; set; }
        [Boolean(Name = "IsCompleted", NullValue = false, Store = true)]
        public virtual bool IsCompleted { get; set; }
    }
}
