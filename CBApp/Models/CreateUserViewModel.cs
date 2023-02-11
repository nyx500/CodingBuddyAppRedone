using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // required for Dictionary DS
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
        [Required(ErrorMessage = "Please enter your career phase!")]
        public int? SelectedCareerPhaseId { get; set; }

        [Display(Name = "Experience Level")]
        [Required(ErrorMessage = "Please enter your experience level!")]
        public int? SelectedExperienceLevelId { get; set; }

        // Gender selection is optional
        [Display(Name = "Gender")]
        public int? SelectedGenderId { get; set; }

        // Multiple checkboxes options (languages & interests)

        [Display(Name = "Select languages:")]
        public List<NaturalLanguageViewModel>? NaturalLanguagesViewModelList {get; set;}

        [Display(Name = "Select your favourite programming languages:")]
        public List<ProgrammingLanguageViewModel>? ProgrammingLanguagesViewModelList { get; set; }

        [Display(Name = "Select your Computer Science interests:")]
        public List<CSInterestViewModel>? CSInterestsViewModelList { get; set; }

        [Display(Name = "Select your favourite hobbies:")]
        public List<HobbyViewModel>? HobbiesViewModelList { get; set; }



    }
}
