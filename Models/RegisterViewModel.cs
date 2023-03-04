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
        [StringLength(100)]
        [Display(Name = "Please enter a password (must be at least 8 chars, may be up to 100 chars)")]
        [Compare("ConfirmPassword")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password!")]
        [StringLength(100)]
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
        public int? SelectedGenderId { get; set; }



        // Multiple checkboxes options (languages & interests)

        [Display(Name = "Select natural/spoken languages: (optional)")]
        public List<NaturalLanguageViewModel>? NaturalLanguagesViewModelList { get; set; }




        [Display(Name = "Select your favourite programming languages (select at least one):")]
        public List<ProgrammingLanguageViewModel>? ProgrammingLanguagesViewModelList { get; set; }



        [Display(Name = "Select your Computer Science interests (please select at least one):")]
        public List<CSInterestViewModel>? CSInterestsViewModelList { get; set; }


        /** Populates the user's list of natural languages with those selected, and saves this to the database */
        /** TO TEST: "exercising data structures unit testing" --> check the following
         * Is the user's list actually populated with the correct options after exiting this function?
         * Does the user data-structure/class instance retain its integrity?
         * Does the DB update properly with the context.saveChanges line of code?
         * What happens if we send 0 options in/0 are selected?
         * What happens if 1000 options are selected?
         * If the FIRST natural language in the list is "selected", is it added properly to the user's list?
         * If the LAST natural language in the list is "selected", is it added properly to the user's list?
        */
        // Parameters: the user who has selected the natural languages to add to their profile, the Database ApplicationDbContext
        public void setSelectedNaturalLanguagesForUser(User user, ApplicationDbContext context)
        {
            // Create a new language list for the user
            user.NaturalLanguageUsers = new List<NaturalLanguageUser>();

            // Loop over the natural languages list stored in this Object/Model
            for (int i = 0; i < NaturalLanguagesViewModelList!.Count; ++i)
            {
                // If a NaturalLanguage view model object in the list has "isSelected" property set to "true"...
                if (NaturalLanguagesViewModelList[i].isSelected)
                {
                    // Id of the language will always be the 'i' index + 1 (starts from 1 not 0) because of how DB is structured
                    // This is bad but could not find a way for the View to return the language itself despite using hidden field, not only the
                    // "isSelected" field
                    NaturalLanguage nLang = context.NaturalLanguages.Find(i + 1)!;

                    // Create many-to-many relationship called "NaturalLanguageUser"
                    NaturalLanguageUser nlUser = new NaturalLanguageUser
                    {
                        NaturalLanguageId = (i + 1),
                        // set the SlackId field in the "natural language" class to the SlackId field in this ViewModel
                        SlackId = SlackId!,
                        User = user,
                        NaturalLanguage = nLang
                    };

                    // Add the just-constructed NaturalLanguageUser to the ICollection in the User class
                    user.NaturalLanguageUsers.Add(nlUser);

                    // Add the many-to-many relationship to the DB context
                    context.NaturalLanguageUsers.Add(nlUser);
                }
            }
        }


        /** Populates the user's list of programming languages with those selected, and saves this to the database */
        public void setSelectedProgrammingLanguagesForUser(User user, ApplicationDbContext context)
        {
            user.ProgrammingLanguageUsers = new List<ProgrammingLanguageUser>();
            for (int i = 0; i < ProgrammingLanguagesViewModelList!.Count; ++i)
            {
                if (ProgrammingLanguagesViewModelList[i].isSelected)
                {

                    ProgrammingLanguage pLang = context.ProgrammingLanguages.Find(i + 1)!;

                    ProgrammingLanguageUser plUser = new ProgrammingLanguageUser
                    {
                        ProgrammingLanguageId = i + 1,
                        SlackId = SlackId!,
                        User = user,
                        ProgrammingLanguage = pLang
                    };


                    // Add a ProgrammingLanguageUser to the ICollection in the User class
                    user.ProgrammingLanguageUsers.Add(plUser);
                    // Add the many-to-many relationship to the context
                    context.ProgrammingLanguageUsers.Add(plUser);

                }
            }
        }

        /** Populates the user's list of CS interests ith those selected, and saves this to the database */
        public void setSelectedComputerScienceInterestsForUser(User user, ApplicationDbContext context)
        {
            user.CSInterestUsers = new List<CSInterestUser>();
            for (int i = 0; i < CSInterestsViewModelList.Count; ++i)
            {
                if (CSInterestsViewModelList[i].isSelected)
                {

                    CSInterest interest = context.CSInterests.Find(i + 1)!;

                    CSInterestUser interestUser = new CSInterestUser
                    {
                        CSInterestId = i + 1,
                        SlackId = SlackId!,
                        User = user,
                        CSInterest = interest
                    };

                    user.CSInterestUsers.Add(interestUser);
                    context.CSInterestUsers.Add(interestUser);

                }
            }
        }

    }
       
}
