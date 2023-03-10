using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CBApp.Data;
using System;
using Microsoft.IdentityModel.Tokens;

namespace CBApp.Models
{

    public class RegisterViewModel
    {
        public List<string> Errors { get; set; }

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


        /***********************FUNCTIONS FOR HTTPGET METHOD ************************************************************/

        // Populates view model with CareerPhase options
        public void getCareerOptionsFromDbContext(ApplicationDbContext context)
        {   
            // Create model list
            careerPhaseSelectList = new List<SelectListItem>();
            // Get options from DB Context
            List<CareerPhase> careerPhases = context.CareerPhases.ToList();

            foreach (var cp in careerPhases)
            {       
                    // Add the select object for career phase options
                    careerPhaseSelectList!.Add(
                    new SelectListItem
                    {
                        Text = cp.Name,
                        Value = cp.CareerPhaseId.ToString()
                    }
                );
            }
        }

        // Populates view model with ExperienceLevels
        public void getExperienceLevelsFromDbContext(ApplicationDbContext context)
        {
            experienceLevelSelectList = new List<SelectListItem>();

            List<ExperienceLevel> experienceLevels = context.ExperienceLevels.ToList();

            foreach (var e in experienceLevels)
            {
                experienceLevelSelectList.Add(
                    new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.ExperienceLevelId.ToString()
                    }
                );
            }
        }

        // Populates view model with Gender options
        public void getGendersFromDbContext(ApplicationDbContext context)
        {

            // Populate view model with Genders
            genderSelectList = new List<SelectListItem>();

            List<Gender> genders = context.Genders.ToList();

            foreach (var g in genders)
            {
                genderSelectList.Add(
                    new SelectListItem
                    {
                        Text = g.Name,
                        Value = g.GenderId.ToString()
                    }
                );
            }
        }

        // Populates view model with Natural Language options
        public void getLanguagesFromDbContext(ApplicationDbContext context)
        {
            List<NaturalLanguage> nlangs = context.NaturalLanguages.ToList();

            NaturalLanguagesViewModelList = new List<NaturalLanguageViewModel>();

            foreach (var language in nlangs)
            {
                NaturalLanguagesViewModelList.Add(
                    new NaturalLanguageViewModel
                    {
                        naturalLanguage = language,
                        isSelected = false
                    }
                );
            }
        }

        // Populates view model with Programming Language options
        public void getProgrammingLanguagesFromDbContext(ApplicationDbContext context)
        {
            // Populates 'create user' view model with programming languages view models with isSelected fields for each language
            List<ProgrammingLanguage> plangs = context.ProgrammingLanguages.ToList();
            ProgrammingLanguagesViewModelList = new List<ProgrammingLanguageViewModel>();
            foreach (var language in plangs)
            {
                ProgrammingLanguagesViewModelList.Add(
                    new ProgrammingLanguageViewModel
                    {
                        programmingLanguage = language,
                        isSelected = false
                    }
                );
            }

        }

        // Populates view model with Comptuer Science interest options
        public void getCSInterestsFromDbContext(ApplicationDbContext context)
        {
            // Populate view model with CS interests
            List<CSInterest> interests = context.CSInterests.ToList();
            CSInterestsViewModelList = new List<CSInterestViewModel>();

            foreach (var interest in interests)
            {
                CSInterestsViewModelList.Add(
                    new CSInterestViewModel
                    {
                        CSInterest = interest,
                        isSelected = false
                    }
                );
            }
        }

        /** Creates the whole model from DB */
        public void createModelFromDatabase(ApplicationDbContext context)
        {
            getCareerOptionsFromDbContext(context);
            getExperienceLevelsFromDbContext(context);
            getGendersFromDbContext(context);
            getLanguagesFromDbContext(context);
            getProgrammingLanguagesFromDbContext(context);
            getCSInterestsFromDbContext(context);
        }
    
        /*********************** END OF FUNCTIONS FOR HTTPGET METHOD *****************************************************/



        /***********************FUNCTIONS FOR HTTPPOST METHOD ************************************************************/
        /** Counts the num of programming langs selected by user */
        public int CountNumberProgrammingLanguagesSelected()
        {
            int counter = 0;
            for (int i = 0; i < ProgrammingLanguagesViewModelList!.Count; ++i)
            {
                if (ProgrammingLanguagesViewModelList[i].isSelected)
                {
                    counter++;
                }
            }
            return counter;
        }

        /** Counts the num of CS interests selected by user */
        public int CountNumberCSInterestsSelected()
        {
            int counter = 0;
            for (int i = 0; i < CSInterestsViewModelList!.Count; ++i)
            {
                if (CSInterestsViewModelList[i].isSelected)
                {
                    counter++;
                }
            }
            return counter;
        }


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
                // Add a new ProgrammingLanguageUser relationship if the ProgrammingLanguage in question has isSelected to true
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

        public void setGenderIfSelected(User user, ApplicationDbContext context)
        {

            // Update Gender "navigation property" of user only if Gender has been selected
            // We know if a gender has been selected if the Id chosen is not 0 (0 is the default value)
            if (SelectedGenderId != 0)
            {
                user.GenderId = SelectedGenderId;
                user.Gender = context.Genders.Find(SelectedGenderId);
            }
            else
            {
                user.GenderId = 0;
                user.Gender = null;
            }

        }


        // Validates format and length of the user's SlackId input
        public List<string> validateSlackId(string SlackId)
        {
            List<string> errorList = new List<string>();

            if (String.IsNullOrEmpty(SlackId))
            {
                errorList.Add("No Slack ID entered");
            }
            else
            {
                if (SlackId!.Length < 5 )
                {
                    errorList.Add("Slack ID must be at least 5 characters");
                }

                if (SlackId.Length > 50)
                {
                    errorList.Add("Slack ID cannot be more than 50 characters");
                }

                if (!SlackId.Any(char.IsDigit))
                {
                    errorList.Add("Slack ID must contain a number");
                }

                if (!SlackId.All(char.IsLetterOrDigit))
                {
                    errorList.Add("Slack ID can only contain letters or digits");
                }
            }
            return errorList;
        }

        // Validates format and length of the user's Username input
        public List<string> validateUsername(string username)
        {
            List<string> errorList = new List<string>();

            // Validates format and length of the user's Username input
            if (String.IsNullOrEmpty(username))
            {
                errorList.Add("No username entered");
            }
            else
            {
                if (username!.Length < 6)
                {
                    errorList.Add("Username has to be more than 6 characters in length");
                }

                if (username!.Length > 14)
                {
                    errorList.Add("Username has to be less than 14 characters in length");
                }

                // Attribution: https://stackoverflow.com/questions/34264226/check-if-a-string-contains-only-letters-digits-and-underscores
                // Checks if UserName contains only alphanumeric or underscores

                bool validateUsernameFormat = username.All(c => Char.IsLetterOrDigit(c) || c.Equals('_'));
                if (!validateUsernameFormat)
                {
                    errorList.Add("Username may only contain letters, digits and underscores");
                }
            }

            return errorList;
        }

        // Validates password fields
        public List<string> validatePassword(string password, string confirmed_password)
        {
            List<string> errorList = new List<string>();

            // Validate password length
            if (password!.Length < 6)
            {
                errorList.Add("Password must be at least 6 characters");
            }

            // If password does not match password confirmation
            if (String.Compare(password, confirmed_password) != 0)
            {
                errorList.Add("Passwords do not match");
            }
            return errorList;
        }

        // Validates Career Phase is Selected
        public List<string> validateCareerPhaseSelected(int id)
        {
            List<string> errorList = new List<string>();

            // Check if career phase has been selected
            if (id == 0)
            {
                errorList.Add("Please select a career phase");
            }

            return errorList;
        }

        // Validates Experience Level is Selected
        public List<string> validateExperienceLevelSelected(int id)
        {
            List<string> errorList = new List<string>();

            // Check if career phase has been selected
            if (id == 0)
            {
                errorList.Add("Please select an experience level");
            }

            return errorList;
        }


        /** Validates the model data which has been returned via the form with httppost */
        public List<string> validateModelData()
        {
            // Create a list of strings describing errors
            List<string> errorList = new List<string>();
            // Adds errors for bad SlackId input
            errorList.AddRange(validateSlackId(SlackId!));
            // Adds errors for bad username input
            errorList.AddRange(validateUsername(UserName!));
            // Adds errors for bad password input
            errorList.AddRange(validatePassword(Password!, ConfirmPassword!));
            // Adds errors if no careerphase/experiencelevel is selected
            errorList.AddRange(validateCareerPhaseSelected(SelectedCareerPhaseId));
            errorList.AddRange(validateExperienceLevelSelected(SelectedExperienceLevelId));

            // If no programming languages selected, append error
            if (CountNumberProgrammingLanguagesSelected() < 1)
            {
                errorList.Add("No programming languages selected. Please select at least one");
            }
            // If no CS interests selected, append error
            if (CountNumberProgrammingLanguagesSelected() < 1)
            {
                errorList.Add("No CS interests selected. Please select at least one");
            }
            return errorList;
        }


        /** Creates a user object and returns it out of the viewModel data */
        public User createUser(ApplicationDbContext context)
        {
            // If the model is valid --> creates a user
            User user = new User
            {
                SlackId = SlackId,
                UserName = UserName,
                CareerPhaseId = SelectedCareerPhaseId,
                CareerPhase = context!.CareerPhases.Find(SelectedCareerPhaseId),
                ExperienceLevelId = SelectedExperienceLevelId,
                ExperienceLevel = context.ExperienceLevels.Find(SelectedExperienceLevelId)
            };

            // Calls the function to set the gender if selected
            setGenderIfSelected(user, context);

            // Adds the language/interest options
            setSelectedNaturalLanguagesForUser(user, context);
            setSelectedProgrammingLanguagesForUser(user, context);
            setSelectedComputerScienceInterestsForUser(user, context);

            // Return the user just created
            return user;

        }

    }
       
}
