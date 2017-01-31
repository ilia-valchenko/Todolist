using System;
using Nest;

namespace BLL.Interfaces.Entities
{ 
    [ElasticsearchType(IdProperty = "Id", Name = "task")]
    public class BllTask
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        [Text(Name = "title")]
        public string Title { get; set; }
        [Text(Name = "description")]
        public string Description { get; set; }
        [Date(Format = "MMddyyyy")]
        public DateTime PublishDate { get; set; }
        [Boolean(NullValue = false, Store = true)]
        public bool IsCompleted { get; set; }
    }
}
