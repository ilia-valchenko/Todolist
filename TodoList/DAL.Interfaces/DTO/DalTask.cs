using System;
using Nest;

namespace DAL.Interfaces.DTO
{
    [ElasticsearchType(IdProperty = "Id", Name = "task")]
    public class DalTask : IEntity
    {
        [Number(Name = "Id")]
        public int Id { get; set; }
        [Text(Name = "Title", Index = false, Store = true, Analyzer = "mynGram")]
        public string Title { get; set; }
        [Text(Name = "Description")]
        public string Description { get; set; }
        [Date(Format = "MMddyyyy", Name = "PublishDate")]
        public DateTime PublishDate { get; set; }
        [Boolean(Name = "IsCompleted", NullValue = false, Store = true)]
        public bool IsCompleted { get; set; }
    }
}
