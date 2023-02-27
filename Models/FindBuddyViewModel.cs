using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CBApp.Models
{
    public class FindBuddyViewModel
    {

        // Checkboxes for filters: career phase, experience level, programming langs, CS intersts and hobbies/interests
        [Display(Name = "Career Phase:")]
        public List<CareerPhaseViewModel>? CareerPhasesViewModelList { get; set; }

        [Display(Name = "Experience Level:")]
        public List<ExperienceLevelViewModel>? ExperienceLevelsViewModelList { get; set; }

        [Display(Name = "Select your favourite programming languages (select at least one):")]
        public List<ProgrammingLanguageViewModel>? ProgrammingLanguagesViewModelList { get; set; }


        [Display(Name = "Select your Computer Science interests (please select at least one):")]
        public List<CSInterestViewModel>? CSInterestsViewModelList { get; set; }

        [Display(Name = "Select your favourite hobbies (please select between **3-10** hobbies!):")]
        public List<HobbyViewModel>? HobbiesViewModelList { get; set; }



    }
}
