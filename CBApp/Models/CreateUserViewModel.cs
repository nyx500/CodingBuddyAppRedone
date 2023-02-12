using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
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


        [Display(Name = "*Career Phase")]
        [Required(ErrorMessage = "Please enter your career phase!")]
        public int? SelectedCareerPhaseId { get; set; }

        [Display(Name = "*Experience Level")]
        [Required(ErrorMessage = "Please enter your experience level!")]
        public int? SelectedExperienceLevelId { get; set; }

        // Gender selection is optional
        [Display(Name = "Gender: (optional)")]
        public int? SelectedGenderId { get; set; }

        // Multiple checkboxes options (languages & interests)

        [Display(Name = "Select natural/spoken languages: (optional)")]
        public List<NaturalLanguageViewModel>? NaturalLanguagesViewModelList {get; set;}

        [Display(Name = "*Select your favourite programming languages (please select at least one):")]
        public List<ProgrammingLanguageViewModel>? ProgrammingLanguagesViewModelList { get; set; }

        [Display(Name = "Select your Computer Science interests (please select at least one):")]
        public List<CSInterestViewModel>? CSInterestsViewModelList { get; set; }

        [Display(Name = "Select your favourite hobbies (please select between **3-10** hobbies!):")]
        public List<HobbyViewModel>? HobbiesViewModelList { get; set; }




    }
}
