using System;

namespace BLL.Models
{ 
    public class BllTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
