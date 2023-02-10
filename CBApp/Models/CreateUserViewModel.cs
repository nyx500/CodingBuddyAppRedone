using System.ComponentModel.DataAnnotations;
namespace CBApp.Models
{   
    public class SelectListItem
    {
        public string? Text { get; set; }
        public string? Value { get; set; }
    }
    public class CreateUserViewModel
    {
        public User? user { get; set; }

        // Dropdowns (single-option)
        public List<SelectListItem>? careerPhaseSelectList;
        public List<SelectListItem>? experienceLevelSelectList;
        public List<SelectListItem>? genderSelectList;


        [Display(Name = "Career Phase")]
        public int? SelectedCareerPhaseId { get; set; }

        [Display(Name = "Experience Level")]
        public int? SelectedExperienceLevelId { get; set; }


        [Display(Name = "Gender")]
        public int? SelectedGenderId { get; set; }

        // Multiple checkboxes

        [Display(Name = "Select languages")]
        public List<NaturalLanguageViewModel>? NaturalLanguagesViewModelList {get; set;}

        [Display(Name = "Select favourite programming languages")]
        public List<ProgrammingLanguageViewModel>? ProgrammingLanguagesViewModelList;
        //public List<CSInterestViewModel>? csInterestViewModelList;
        //public List<HobbyViewModel>? hobbiesViewModelList;

    }
}
