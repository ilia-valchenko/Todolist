using System;
using Nest;

namespace DAL.Entities
{
    [ElasticsearchType(IdProperty = "Id", Name = "task")]
    public class DalTask 
    {
        [Number(Name = "Id")]
        public virtual int Id { get; set; }
        [String(Name = "Title", Analyzer = "customIndexNgramAnalyzer", SearchAnalyzer = "customSearchNgramAnalyzer", IndexOptions = IndexOptions.Offsets)]
        public virtual string Title { get; set; }
        [String(Name = "Description")]
        public virtual string Description { get; set; }
        //[Date(Format = "MMddyyyy", Name = "PublishDate")]
        [Date(Name = "PublishDate")]
        public virtual DateTime PublishDate { get; set; }
        [Boolean(Name = "IsCompleted", NullValue = false, Store = true)]
        public virtual bool IsCompleted { get; set; }
    }
}
