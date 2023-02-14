using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CBApp.Data;
using System;

namespace CBApp.Models
{

    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Please enter a Slack ID!")]
        [StringLength(50)]
        [Display(Name = "Please enter your Slack ID (at least 5 chars, alphanumeric only)")]
        public string? SlackId { get; set; }

        [Required(ErrorMessage = "Please enter a Username!")]
        [StringLength(70)]
        [Display(Name = "Choose a username (must be at least 6 chars)")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "You must enter a password!")]
        [DataType(DataType.Password)]
        [Display(Name = "Please enter a password (must be at least 8 chars)")]
        [Compare("ConfirmPassword")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

        // Dropdowns (single-option)
        public List<SelectListItem>? careerPhaseSelectList;
        public List<SelectListItem>? experienceLevelSelectList;
        public List<SelectListItem>? genderSelectList;


        [Display(Name = "*Career Phase")]
        [Required(ErrorMessage = "Please enter your career phase!")]
        public int SelectedCareerPhaseId { get; set; }

        [Display(Name = "*Experience Level")]
        [Required(ErrorMessage = "Please enter your experience level!")]
        public int SelectedExperienceLevelId { get; set; }

        // Gender selection is optional
        [Display(Name = "Gender: (optional)")]
        public int SelectedGenderId { get; set; }



        // Multiple checkboxes options (languages & interests)

        [Display(Name = "Select natural/spoken languages: (optional)")]
        public List<NaturalLanguageViewModel>? NaturalLanguagesViewModelList { get; set; }

       


        [Display(Name = "Select your favourite programming languages (select at least one):")]
        public List<ProgrammingLanguageViewModel>? ProgrammingLanguagesViewModelList { get; set; }



        [Display(Name = "Select your Computer Science interests (please select at least one):")]
        public List<CSInterestViewModel>? CSInterestsViewModelList { get; set; }

        

        [Display(Name = "Select your favourite hobbies (please select between **3-10** hobbies!):")]
        public List<HobbyViewModel>? HobbiesViewModelList { get; set; }


    }
}
