using System.ComponentModel.DataAnnotations;

namespace RESTService.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required(ErrorMessage = "The title can't be empty!")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}