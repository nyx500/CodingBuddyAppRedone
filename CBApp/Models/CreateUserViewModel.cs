namespace CBApp.Models
{
    public class CreateUserViewModel
    {
        public User user { get; set; }

        public List<CareerPhase> careerPhaseList;
        public List<ExperienceLevelViewModel> experienceLevelViewModelList;
        public List<NaturalLanguageViewModel> naturalLanguagesViewModelList;
        public List<GenderViewModel> genderViewModelList;

        public List<ProgrammingLanguageViewModel> programmingLanguagesViewModelList;
        public List<CSInterestViewModel> csInterestViewModelList;
        public List<HobbyViewModel> hobbiesViewModelList;

    }
}
