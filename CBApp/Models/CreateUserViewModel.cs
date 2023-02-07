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
        public List<ExperienceLevel>? experienceLevelList;
        public List<Gender>? genderList;

        public int? SelectedCareerPhaseId { get; set; }
        public ExperienceLevel? SelectedExperienceLevelId { get; set; }
        public Gender? SelectedGenderId { get; set; }

        // Multiple checkboxes
        public List<NaturalLanguageViewModel>? naturalLanguagesViewModelList;
        public List<ProgrammingLanguageViewModel>? programmingLanguagesViewModelList;
        public List<CSInterestViewModel>? csInterestViewModelList;
        public List<HobbyViewModel>? hobbiesViewModelList;

    }
}
