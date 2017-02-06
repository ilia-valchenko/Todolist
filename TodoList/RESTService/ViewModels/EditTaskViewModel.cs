using System.ComponentModel.DataAnnotations;

namespace RESTService.ViewModels
{
    public class EditTaskViewModel
    {
        [Required(ErrorMessage = "Editable task must has an Id!")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The title can't be empty!")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}