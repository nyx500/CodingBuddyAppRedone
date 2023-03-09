using System.ComponentModel.DataAnnotations;
using CBApp.Data;
using Microsoft.AspNetCore.Http;

namespace CBApp.Models
{
    public class EditUserViewModel
    {
        // Stores the errors if the user data is invalid
        public List<string> Errors = new List<string>();
        public string? ImageSrc { get; set; }
        public string? UserName { get; set;}
        public string? SlackId { get; set; }

        public string? Bio { get; set; }

        public CareerPhase? CareerPhase { get; set; }
        public ExperienceLevel? ExperienceLevel { get; set; }
        public Gender? Gender { get; set; }
        public List<String>? LanguageNames { get; set; }
        public List<String>? ProgrammingLanguageNames { get; set; }
        public List<String>? CSInterestNames { get; set; }
        public List<String>? HobbyNames { get; set; }
        public List<QuestionAnswerBlock>? QuestionAnswerBlocks { get; set; }
        public List<NaturalLanguageViewModel>? NaturalLanguagesViewModelList { get; set; }
        public List<ProgrammingLanguageViewModel>? ProgrammingLanguagesViewModelList { get; set; }
        public List<CSInterestViewModel>? CSInterestsViewModelList { get; set; }
        public List<HobbyViewModel>? HobbiesViewModelList { get; set; }


        // Dropdowns and selections (single-option) for editing the choices
        public List<SelectListItem>? careerPhaseSelectList;
        public List<SelectListItem>? experienceLevelSelectList;
        public List<SelectListItem>? genderSelectList;
        public int SelectedCareerPhaseId { get; set; }
        public int SelectedExperienceLevelId { get; set; }
        public int SelectedGenderId { get; set; }


        // Checks if the user exists
        public bool checkUserExists(User user)
        {
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void createModelFromUser(User user)
        {   
            // Checks if the user exists, records error if they do not
            if (!checkUserExists(user))
            {
                Errors.Add("This user does not exist in the database.");
            }

            // Check if username for user exists, adds error if does not
            if (checkUsernameIsValid(user.UserName))
            {
                UserName = user.UserName;
            }
            else
            {
                Errors.Add("This user does not have a valid username");
            }

            // Checks if Bio for user exists, set the field to null if not
            if (checkBioExists(user.Bio))
            {
                Bio = user.Bio;
            }
            else
            {
                Bio = null;
            }

            // Checks if Slack Id exists, add Error to model if not
            if (checkSlackIdIsValid(user.SlackId))
            {
                SlackId = user.SlackId;
            }
            else
            {
                Errors.Add("This user does not have a valid Slack ID");
            }

            // Checks if user has a career phase, sets error if they do not
            if (checkCareerPhaseIdIsValid(user.CareerPhaseId))
            {
                CareerPhase = user.CareerPhase;
            }
            else
            {
                Errors.Add("This user does not have a valid Career Phase selected");
            }

            if (checkExperienceLevelIdIsValid(user.ExperienceLevelId))
            {
                ExperienceLevel = user.ExperienceLevel;
            }
            else
            {
                Errors.Add("This user does not have a valid Experience Level selected");
            }
            
            // Optional gender field: set it to null if not selected
            if (checkGenderIdIsValid(user.GenderId))
            {
                Gender = user.Gender;
            }
            else
            {
                Gender = null;
            }

            // Set fields for languages/interests and question answer blocks from user
            createQuestionAnswerList(user.QuestionAnswerBlocks!.ToList());
            createLanguagesList(user.NaturalLanguageUsers!.ToList());
            createProgrammingLanguagesList(user.ProgrammingLanguageUsers!.ToList());
            createCSInterestsList(user.CSInterestUsers!.ToList());
            createHobbiesList(user.HobbyUsers!.ToList());
            setPictureString(user.PictureFormat!, user.Picture);
        }

        // Checks if the user's username exists, returns true if it does, else returns false
        public bool checkUsernameIsValid(string username)
        {
            if (username == null || username.Length < 1) 
            { return false; }
            else 
            { return true; }
        }

        // Checks if the user's SlackId exists, returns true if it does, else returns false
        public bool checkSlackIdIsValid(string slackId)
        {
            if (slackId == null || slackId.Length < 1)
            { return false; }
            else 
            { return true; }

        }

        // Checks if the user's Bio has been filled in, returns true if it does, else returns false
        public bool checkBioExists(string bio)
        {
            if (bio == null || bio.Length < 1)
            { return false; }
            else
            { return true; }

        }


        // Checks if the CareerPhase has been selected ( = Id is not 0), returns true if it does, else returns false
        public bool checkCareerPhaseIdIsValid(int careerPhaseId)
        {
            if (careerPhaseId == 0)
            { return false; }
            else
            { return true; }

        }

        // Checks if the ExperienceLevel has been selected ( = Id is not 0), returns true if it does, else returns false
        public bool checkExperienceLevelIdIsValid(int experienceLevelId)
        {
            if (experienceLevelId == 0)
            { return false; }
            else
            { return true; }

        }


        // Checks if the Gender has been selected ( = Id is not 0), returns true if it does, else returns false
        public bool checkGenderIdIsValid(int? genderId)
        {
            if (genderId == 0 || genderId == null)
            { return false; }
            else
            { return true; }
        }


        // Fills in the model's Question-Answer blocks from user
        public void createQuestionAnswerList(List<QuestionAnswerBlock> qablocks)
        {
            // Set field to an empty list 
            QuestionAnswerBlocks = new List<QuestionAnswerBlock>();
            // Populate it with the user's data
            foreach (QuestionAnswerBlock qaBlock in qablocks)
            {
                QuestionAnswerBlocks.Add(qaBlock);
            }
        }

        // Fills in the model's Natural Languages (as strings) from user
        public void createLanguagesList(List<NaturalLanguageUser> natlangusers)
        {
            // Set field to an empty list of strings
            LanguageNames = new List<string>();
            // Populate it with the user's data
            foreach (NaturalLanguageUser n in natlangusers)
            {
                string languageName = n.NaturalLanguage.Name;
                LanguageNames.Add(languageName);
            }
        }

        // Fills in the model's Programming Languages (as strings) from user
        public void createProgrammingLanguagesList(List<ProgrammingLanguageUser> proglangusers)
        {
            ProgrammingLanguageNames = new List<String>();
            foreach (ProgrammingLanguageUser p in proglangusers)
            {
                string languageName = p.ProgrammingLanguage.Name;
                ProgrammingLanguageNames.Add(languageName);
            }

            if (ProgrammingLanguageNames.Count() < 1)
            {
                Errors.Add("User has not selected any programming languages!");
            }
        }

        // Fills in the model's CS Interests (as strings) from user
        public void createCSInterestsList(List<CSInterestUser> csinterestusers)
        {
            CSInterestNames = new List<String>();
            foreach (CSInterestUser c in csinterestusers)
            {
                string interest = c.CSInterest.Name;
                CSInterestNames.Add(interest);
            }

            if (CSInterestNames.Count() < 1)
            {
                Errors.Add("User has not selected any Computer Science interests!");
            }
        }

        // Fills in the model's Hobbies (as strings) from user
        public void createHobbiesList(List<HobbyUser> hobbyusers)
        {
            HobbyNames = new List<String>();
            foreach (HobbyUser h in hobbyusers)
            {
                string hobbyName = h.Hobby.Name;
                HobbyNames.Add(hobbyName);
            }

        }

        // Sets picture data (if user has a profile picture) to a string readable in the HTML file
        public void setPictureString(string pictureFormat, byte[]? picture)
        {
            if (pictureFormat != null)
            {
                // Attribution: converting bytes to image (https://stackoverflow.com/questions/17952514/asp-net-mvc-how-to-display-a-byte-array-image-from-model)
                // Gets the mime type of the picture
                string picFormat = pictureFormat!.Substring(1);
                // Converts the mimetype to a string readable in the HTML file
                string mimeType = "image/" + picFormat;
                // Convert the picture byte array to a string readable in the HTML file
                string base64 = Convert.ToBase64String(picture!);
                ImageSrc = string.Format("data:{0};base64,{1}", mimeType, base64);
            }
            // If user has no picture, set the ImageSrc property to null
            else
            {
                ImageSrc = null;
            }

        }


        /** Functions to create dropdowns/checkboxes for the user to edit their preferences from the ApplicationDbContext */
        // Populates all the edit-preferences groups of options
        public void createOptionsLists(ApplicationDbContext context)
        {
            getCareerPhasesFromDb(context);
            getExperienceLevelsFromDb(context);
            getGendersFromDb(context);
            getNaturalLangsFromDb(context);
            getProgrammingLangsFromDb(context);
            getCSInterestsFromDb(context);
            getHobbiesFromDb(context);
        }

        // Gets all Career Phases from DB and stores them as SelectListItems with a text + value property for the dropdown
        public void getCareerPhasesFromDb(ApplicationDbContext context)
        {
            careerPhaseSelectList = new List<SelectListItem>();
            List<CareerPhase> careerPhases = context.CareerPhases.ToList();
            foreach (var cp in careerPhases)
            {
                    careerPhaseSelectList.Add(
                    new SelectListItem
                    {
                        Text = cp.Name,
                        Value = cp.CareerPhaseId.ToString()
                    }
                ); ;
            }
        }

        // Gets all ExperienceLevels from DB and stores them as SelectListItems with a text + value property for the dropdown
        public void getExperienceLevelsFromDb(ApplicationDbContext context)
        {
            // Populate the view model with ExperienceLevels
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
                ); ;
            }
        }

        // Gets all Genders from DB and stores them as SelectListItems with a text + value property for the dropdown
        public void getGendersFromDb(ApplicationDbContext context)
        {
            // Populate the view model with Genders
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
                ); ;
            }

        }

        // Gets all natural languages from DB and stores them in the model with an isSelected field for checkboxes
        public void getNaturalLangsFromDb(ApplicationDbContext context)
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

        // Gets all programming languages from DB and stores them in the model with an isSelected field for checkboxes
        public void getProgrammingLangsFromDb(ApplicationDbContext context)
        {
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

        // Gets all CS interests from DB and stores them in the model with an isSelected field for checkboxes
        public void getCSInterestsFromDb(ApplicationDbContext context)
        {
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

        // Gets all hobbies from DB and stores them in the model with an isSelected field for checkboxes
        public void getHobbiesFromDb(ApplicationDbContext context)
        {
            List<Hobby> hobbies = context.Hobbies.ToList();
            HobbiesViewModelList = new List<HobbyViewModel>();
            foreach (var hobby in hobbies)
            {
                HobbiesViewModelList.Add(
                    new HobbyViewModel
                    {
                        Hobby = hobby,
                        isSelected = false
                    }
                );
            }
        }


    }
}
