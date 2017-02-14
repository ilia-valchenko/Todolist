using System;
using System.ComponentModel.DataAnnotations;

namespace RESTService.ViewModels
{
    public class EditTaskViewModel
    {
        [Required(ErrorMessage = "Editable task must have an Id!")]
        [Range(1, int.MaxValue, ErrorMessage = "The value of Id must be greater than {1}")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The title can't be empty!")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}