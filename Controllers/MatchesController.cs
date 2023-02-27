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

            // Error-reporting functionality based on the "FormErrors" class which stores Booleans for different error types
            // Clears the error list if the user refreshes the form or navigates from a different section of the website
            if (HttpContext.Session.GetInt32("RedirectToFindABuddyForm") != null)
            {
                // Check if the current action is being called because of an invalid POST request for the same action
                if (HttpContext.Session.GetInt32("RedirectToFindABuddyForm") != 1)
                {
                    // Clear the error object if the current httpget action is not being executed due to an invalid form request
                    if (HttpContext.Session.GetObject<FormErrors>("FindABuddyErrors") != null)
                    {
                        FormErrors emptyErrorList = new FormErrors();
                        HttpContext.Session.SetObject("FindABuddyErrors", emptyErrorList);
                    }
                }
            }

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

            // Set the session variable that tracks if this was a redirect due to an invalid form submit to 0 (means false in this context)
            HttpContext.Session.SetInt32("RedirectToFindABuddyForm", 0);

            return View(model);

        }
    }
}
