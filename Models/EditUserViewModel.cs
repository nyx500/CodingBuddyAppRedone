using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CBApp.Models
{
    public class EditUserViewModel
    {
        public string? ImageSrc { get; set; }
        public string? UserName { get; set;}
        public string? SlackId { get; set; }

        public string? Bio { get; set; }

        public CareerPhase? CareerPhase { get; set; }
        public ExperienceLevel? ExperienceLevel { get; set; }
        public List<String>? LanguageNames { get; set; }
        public List<String>? ProgrammingLanguageNames { get; set; }
        public List<String>? CSInterestNames { get; set; }
        public List<String>? HobbyNames { get; set; }
        public List<NaturalLanguageViewModel>? NaturalLanguagesViewModelList { get; set; }
        public List<ProgrammingLanguageViewModel>? ProgrammingLanguagesViewModelList { get; set; }
        public List<CSInterestViewModel>? CSInterestsViewModelList { get; set; }
        public List<HobbyViewModel>? HobbiesViewModelList { get; set; }


        // Dropdowns and selections (single-option) for editing the choices
        public List<SelectListItem>? careerPhaseSelectList;
        public List<SelectListItem>? experienceLevelSelectList;
        public int SelectedCareerPhaseId { get; set; }
        public int SelectedExperienceLevelId { get; set; }

    }
}
