using System;

namespace ORM.Models
{
    public class Task
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual bool IsCompleted { get; set; }
    }
}
