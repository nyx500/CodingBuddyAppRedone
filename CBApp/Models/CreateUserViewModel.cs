namespace CBApp.Models
{
    public class CreateUserViewModel
    {
        public User user { get; set; }

        List<CareerPhaseViewModel> careerPhaseViewModelList;
        List<ExperienceLevelViewModel> experienceLevelViewModelList;
        List<NaturalLanguageViewModel> naturalLanguagesViewModelList;
        List<Gender> genderViewModelList;

        List<ProgrammingLanguageViewModel> programmingLangsViewModelList;
        List<CSInterestViewModel> csInterestViewModelList;
        List<HobbyViewModel> hobbiesViewModelList;

    }
}
