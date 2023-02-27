using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CBApp.Models;
using CBApp.Data;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Security.Claims;

namespace CBApp.Controllers
{
    public class MatchesController : Controller
    {

        /**
        * Provides the Database Context object passed in through constructor’s parameter,
        * this will provide the controller with the Database Context object through a
        * Dependency Injection.
     */
        private ApplicationDbContext? context;
        public MatchesController(ApplicationDbContext _context, UserManager<User> _userManager,
            SignInManager<User> _signInManager, IWebHostEnvironment _environment)
        {   
            // Set up database context and Identity User inbuilt functionality for the controller
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
            Environment = _environment;
        }

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IWebHostEnvironment Environment;

        [Authorize]
        [HttpGet]
        // Goes to the Find-a-Buddy form
        public IActionResult FindABuddy()
        {

            // Creates a new Find-a-Buddy View Model
            FindBuddyViewModel model = new FindBuddyViewModel();


            // Populates the view model with Career Phase view models with isSelected fields for checkbox functionality in View
            List<CareerPhase> phases = context.CareerPhases.ToList();
            model.CareerPhasesViewModelList = new List<CareerPhaseViewModel>();
            foreach (var ph in phases)
            {
                model.CareerPhasesViewModelList.Add(
                    new CareerPhaseViewModel
                    {
                        careerPhase = new CareerPhase{
                            CareerPhaseId = ph.CareerPhaseId, 
                            // Put "Career " next to Starter/Changer/Developer
                            Name = "Career " + ph.Name,
                        },
                        isSelected = false
                    }
                );
            }

            // Populates the view model with Experience Level view models with isSelected fields for checkbox functionality in View
            List<ExperienceLevel> levels = context.ExperienceLevels.ToList();
            model.ExperienceLevelsViewModelList = new List<ExperienceLevelViewModel>();
            foreach (var lev in levels)
            {
                model.ExperienceLevelsViewModelList.Add(
                    new ExperienceLevelViewModel
                    {
                        experienceLevel = lev,
                        isSelected = false
                    }
                );
            }
            // Populates the view model with programming languages view models with isSelected fields for checkbox functionality in View
            List<ProgrammingLanguage> plangs = context.ProgrammingLanguages.ToList();
            model.ProgrammingLanguagesViewModelList = new List<ProgrammingLanguageViewModel>();
            foreach (var language in plangs)
            {
                model.ProgrammingLanguagesViewModelList.Add(
                    new ProgrammingLanguageViewModel
                    {
                        programmingLanguage = language,
                        isSelected = false
                    }
                );
            }


            // Populates the view model with CSInterest view models with isSelected fields for checkbox functionality in View
            List<CSInterest> interests = context.CSInterests.ToList();
            model.CSInterestsViewModelList = new List<CSInterestViewModel>();

            foreach (var interest in interests)
            {
                model.CSInterestsViewModelList.Add(
                    new CSInterestViewModel
                    {
                        CSInterest = interest,
                        isSelected = false
                    }
                );
            }


            // Populates the view model with Hobby view models with isSelected fields for checkbox functionality in View
            List<Hobby> hobbies = context.Hobbies.ToList();
            model.HobbiesViewModelList = new List<HobbyViewModel>();


            foreach (var hobby in hobbies)
            {
                model.HobbiesViewModelList.Add(
                    new HobbyViewModel
                    {
                        Hobby = hobby,
                        isSelected = false
                    }
                );
            }

            return View(model);

        }




        [Authorize]
        [HttpPost]
        // Goes to the Find-a-Buddy form
        public IActionResult FindABuddy(FindBuddyViewModel model)
        {
            PotentialMatchesViewModel unsortedModel = new PotentialMatchesViewModel();

            // List of selected career phase Ids
            List<int> SelectedCareerPhaseIds = new List<int>();

            // Model will never return actual careerPhase field (just isSelected bool value),
            // so store the id in a separate int
            int careerId = 1;
            foreach (var career_phase_view_model in model.CareerPhasesViewModelList!)
            {
                if (career_phase_view_model.isSelected)
                {
                        //Add selected career phase Id to filter list
                        SelectedCareerPhaseIds.Add(careerId);
                }
                // Increment to get the next careerPhase Id
                ++careerId;
            }

            // List of selected experience level Ids
            List<int> SelectedExperienceLevelIds = new List<int>();
            int experienceLevelId = 1;
            foreach (var exp_view_model in model.ExperienceLevelsViewModelList!)
            {
                if (exp_view_model.isSelected)
                {
                    // Add selected experience level Id to filter list
                    SelectedExperienceLevelIds.Add(experienceLevelId);
                }
                ++experienceLevelId;
            }

            // List of selected programming language Ids
            List<int> SelectedProgrammingLanguageIds = new List<int>();
            int progLangId = 1;
            foreach (var p_lang_view_model in model.ProgrammingLanguagesViewModelList!)
            {
                if (p_lang_view_model.isSelected)
                {
                    // Add selected programming language Id to filter list
                    SelectedProgrammingLanguageIds.Add(progLangId);
                }
                ++progLangId;
            }

            // List of selected CS Interest Ids
            List<int> SelectedCSInterestsIds = new List<int>();
            int csInterestId = 1;
            foreach (var cs_view_model in model.CSInterestsViewModelList!)
            {
                if (cs_view_model.isSelected)
                {
                    // Add selected CS Interest Id to filter list
                    SelectedCSInterestsIds.Add(csInterestId);
                }
                ++csInterestId;
            }


            // List of selected hobby Ids
            List<int> SelectedHobbyIds = new List<int>();
            int hobbyId = 1;
            foreach (var hobby_view_model in model.HobbiesViewModelList!)
            {
                if (hobby_view_model.isSelected)
                {
                    // Add selected programming language Id to filter list
                    SelectedHobbyIds.Add(hobbyId);
                }
                ++hobbyId;
            }

            // Now find potential matches from users in database
            List<User> users = context.Users.ToList();

            // If all the inputs were empty, just return all the users
            if (SelectedCareerPhaseIds.Count == 0 && SelectedExperienceLevelIds.Count == 0
                && SelectedProgrammingLanguageIds.Count == 0 && SelectedCSInterestsIds.Count == 0
                && SelectedHobbyIds.Count == 0)
            {
                return Content(users.Count.ToString());
            }


            // Go through the user's selected career phase ids and add the users who match to the model
            if (SelectedCareerPhaseIds.Count > 0)
            {
                foreach (User user in users)
                {
                    // Add the user to 'users' if their careerPhaseId is one of the ones selected
                    if (SelectedCareerPhaseIds.Contains(user.CareerPhaseId))
                    {

                        // Check if the potentialMatch has already been added to the list of matches by comparing Ids that have been added
                        // findIndex List method returns -1 if it doesn't find the object with the predicate specified, otherwise returns its index
                        var userIndex = unsortedModel.matches.FindIndex(m => m.Username == user.UserName);
                        // If user has not been added to model list yet, then add the user
                        if (userIndex == -1)
                        {    
                            // Turn user into a PotentialMatchUserViewModel to add them to the model
                            PotentialMatchUserViewModel potentialMatch = PotentialMatchUserViewModel.parseUser(user);
                            // Add CS interests to the potentialMatch
                            foreach (CSInterestUser csiu in user.CSInterestUsers)
                            {
                                potentialMatch.Interests.Add(csiu.CSInterest);
                            }

                            // Add 1 to the potential user's match-score
                            potentialMatch.score = 1;
                            unsortedModel.matches.Add(potentialMatch);
                        }
                        // User is found in the model already --> increment their score by 1
                        else
                        {
                            unsortedModel.matches[userIndex].score += 1;
                        }

                    }
                }
            }

            // Go through the user's selected experience level ids and add the users who match to the model
            if (SelectedExperienceLevelIds.Count > 0)
            {
                foreach (User user in users)
                {
                    // Add the user to 'users' if their careerPhaseId is one of the ones selected
                    if (SelectedExperienceLevelIds.Contains(user.ExperienceLevelId))
                    {

                        // Check if the potentialMatch has already been added to the list of matches by comparing Ids that have been added
                        // findIndex List method returns -1 if it doesn't find the object with the predicate specified, otherwise returns its index
                        var userIndex = unsortedModel.matches.FindIndex(m => m.Id == user.Id);
                        // If user has not been added to model list yet, then add the user
                        if (userIndex == -1)
                        {
                            // Turn user into a PotentialMatchUserViewModel to add them to the model
                            PotentialMatchUserViewModel potentialMatch = PotentialMatchUserViewModel.parseUser(user);
                            // Add CS interests to the potentialMatch
                            foreach (CSInterestUser csiu in user.CSInterestUsers)
                            {
                                potentialMatch.Interests.Add(csiu.CSInterest);
                            }

                            // Add 1 to the potential user's match-score
                            potentialMatch.score = 1;
                            unsortedModel.matches.Add(potentialMatch);
                        }
                        // If the user is found in the model list already, then increment their score by 1
                        else
                        {
                            unsortedModel.matches[userIndex].score += 1;
                        }

                    }
                }
            }


            // Now for more complex fields: see if a user matches with programming languages
            // Go through the user's selected experience level ids and add the users who match to the model
            if (SelectedProgrammingLanguageIds.Count > 0)
            {
                foreach (User user in users)
                {
                    // One more step compared to single-value properties: have to also loop through a user's many programming languages
                    foreach (ProgrammingLanguageUser u in user.ProgrammingLanguageUsers)
                    {
                        if (SelectedProgrammingLanguageIds.Contains(u.ProgrammingLanguageId))
                        {
                            // Check if the potentialMatch has already been added to the list of matches by comparing Ids that have been added
                            // findIndex List method returns -1 if it doesn't find the object with the predicate specified, otherwise returns its index
                            var userIndex = unsortedModel.matches.FindIndex(m => m.Id == user.Id);

                            // If user has not been added to model list yet, then add the user
                            if (userIndex == -1)
                            {
                                PotentialMatchUserViewModel potentialMatch = PotentialMatchUserViewModel.parseUser(user);
                                foreach (CSInterestUser csiu in user.CSInterestUsers)
                                {
                                    potentialMatch.Interests.Add(csiu.CSInterest);
                                }
                                potentialMatch.score = 1;
                                unsortedModel.matches.Add(potentialMatch);
                            }
                            else
                            {
                                unsortedModel.matches[userIndex].score += 1;
                            }

                        }
                    }
                }
            }
            if (SelectedCSInterestsIds.Count > 0)
            {
                foreach (User user in users)
                {
                    // One more step compared to single-value properties: have to also loop through a user's many programming languages
                    foreach (CSInterestUser u in user.CSInterestUsers)
                    {
                        if (SelectedCSInterestsIds.Contains(u.CSInterestId))
                        {
                            // Check if the potentialMatch has already been added to the list of matches by comparing Ids that have been added
                            // findIndex List method returns -1 if it doesn't find the object with the predicate specified, otherwise returns its index
                            var userIndex = unsortedModel.matches.FindIndex(m => m.Id == user.Id);

                            // If user has not been added to model list yet, then add the user
                            if (userIndex == -1)
                            {
                                PotentialMatchUserViewModel potentialMatch = PotentialMatchUserViewModel.parseUser(user);
                                foreach (CSInterestUser csiu in user.CSInterestUsers)
                                {
                                    potentialMatch.Interests.Add(csiu.CSInterest);
                                }
                                potentialMatch.score = 1;
                                unsortedModel.matches.Add(potentialMatch);
                            }
                            else
                            {
                                unsortedModel.matches[userIndex].score += 1;
                            }

                        }
                    }
                }
            }

            if (SelectedHobbyIds.Count > 0)
            {
                foreach (User user in users)
                {
                    // One more step compared to single-value properties: have to also loop through a user's many programming languages
                    foreach (HobbyUser u in user.HobbyUsers)
                    {
                        if (SelectedHobbyIds.Contains(u.HobbyId))
                        {
                            // Check if the potentialMatch has already been added to the list of matches by comparing Ids that have been added
                            // findIndex List method returns -1 if it doesn't find the object with the predicate specified, otherwise returns its index
                            var userIndex = unsortedModel.matches.FindIndex(m => m.Id == user.Id);

                            // If user has not been added to model list yet, then add the user
                            if (userIndex == -1)
                            {
                                PotentialMatchUserViewModel potentialMatch = PotentialMatchUserViewModel.parseUser(user);
                                foreach (CSInterestUser csiu in user.CSInterestUsers)
                                {
                                    potentialMatch.Interests.Add(csiu.CSInterest);
                                }
                                potentialMatch.score = 1;
                                unsortedModel.matches.Add(potentialMatch);
                            }
                            else
                            {
                                unsortedModel.matches[userIndex].score += 1;
                            }

                        }
                    }
                }
            }


            PotentialMatchesViewModel sortedModel = new PotentialMatchesViewModel();
            // Sort the list of matches by score, so better-matched user appear first
            sortedModel.matches = unsortedModel.matches.OrderBy(u => u.score).ToList();

            return View("PotentialMatches", sortedModel);
        }
    }
}
