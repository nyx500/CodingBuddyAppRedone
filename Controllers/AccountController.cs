using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CBApp.Models;
using CBApp.Data;

namespace CBApp.Controllers
{
    public class AccountController : Controller
    {

        /**
         * Provides the Database Context object passed in through constructor’s parameter,
         * this will provide the controller with the Database Context object through a
         * Dependency Injection.
      */
        private ApplicationDbContext? context;
        public AccountController(ApplicationDbContext _context)
        {
            context = _context;
        }

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        [HttpGet]
        public IActionResult Register()
        {
            // Logic to clear error list if user refreshes the page (to not highlight fields with bad input in red)
            if (HttpContext.Session.GetInt32("ThisIsARedirect") != null)
            {
                if (HttpContext.Session.GetInt32("ThisIsARedirect") != 1)
                {
                    // Clear the error list if action method is not being called due to redirect from HttpPost method (bad input)
                    if (HttpContext.Session.GetObject<CreateUserErrors>("InputErrors") != null)
                    {
                        CreateUserErrors emptyErrorList = new CreateUserErrors();
                        HttpContext.Session.SetObject("InputErrors", emptyErrorList);
                    }
                }
            }

            RegisterViewModel model = new RegisterViewModel();

            // Populate view model with CareerPhases
            model.careerPhaseSelectList = new List<SelectListItem>();

            List<CareerPhase> careerPhases = context.CareerPhases.ToList();

            foreach (var cp in careerPhases)
            {
                model.careerPhaseSelectList.Add(
                    new SelectListItem
                    {
                        Text = cp.Name,
                        Value = cp.CareerPhaseId.ToString()
                    }
                ); ;
            }

            // Populate view model with ExperienceLevels
            model.experienceLevelSelectList = new List<SelectListItem>();

            List<ExperienceLevel> experienceLevels = context.ExperienceLevels.ToList();

            foreach (var e in experienceLevels)
            {
                model.experienceLevelSelectList.Add(
                    new SelectListItem
                    {
                        Text = e.Name,
                        Value = e.ExperienceLevelId.ToString()
                    }
                ); ;
            }


            // Populate view model with Genders
            model.genderSelectList = new List<SelectListItem>();
            List<Gender> genders = context.Genders.ToList();

            foreach (var g in genders)
            {
                model.genderSelectList.Add(
                    new SelectListItem
                    {
                        Text = g.Name,
                        Value = g.GenderId.ToString()
                    }
                );
            }

            List<NaturalLanguage> nlangs = context.NaturalLanguages.ToList();

            model.NaturalLanguagesViewModelList = new List<NaturalLanguageViewModel>();

            foreach (var language in nlangs)
            {
                model.NaturalLanguagesViewModelList.Add(
                    new NaturalLanguageViewModel
                    {
                        naturalLanguage = language,
                        isSelected = false
                    }
                );
            }

            // Populates 'create user' view model with programming languages view models with isSelected fields for each language
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


            // Populate view model with CS interests
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


            // Populate view model with hobbies
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

            HttpContext.Session.SetInt32("isRedirect", 0);

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {


            string contentString = "";

            var username = model.UserName;

            contentString += " " + username + ", ";

            var SlackId = model.SlackId;
            contentString += " " + SlackId + ", ";


            int selectedCareerPhase = model.SelectedCareerPhaseId;
            contentString += " " + selectedCareerPhase.ToString() + ", ";

            int selectedExperienceLevel = model.SelectedExperienceLevelId;
            contentString += " " + selectedExperienceLevel.ToString() + ", ";

            int selectedGender = model.SelectedGenderId;
            contentString += " " + selectedGender.ToString() + ", ";

            // Populate view model with CareerPhases
            model.careerPhaseSelectList = new List<SelectListItem>();

            List<CareerPhase> careerPhases = context.CareerPhases.ToList();

            foreach (var cp in careerPhases)
            {
                model.careerPhaseSelectList.Add(
                    new SelectListItem
                    {
                        Text = cp.Name,
                        Value = cp.CareerPhaseId.ToString()
                    }
                ); ;
            }

 

            for (int i = 0; i < model.NaturalLanguagesViewModelList!.Count; i++)
            {
                if (model.NaturalLanguagesViewModelList[i].isSelected)
                {
                    NaturalLanguage nl = context!.NaturalLanguages.Find(i + 1)!;
                    contentString += " " + nl.Name + ", ";
                }
            }

            for (int i = 0; i < model.ProgrammingLanguagesViewModelList!.Count; i++)
            {
                if (model.ProgrammingLanguagesViewModelList[i].isSelected)
                {
                    ProgrammingLanguage pl = context!.ProgrammingLanguages.Find(i + 1)!;
                    contentString += " " + pl.Name + ", ";
                }
            }

            for (int i = 0; i < model.CSInterestsViewModelList!.Count; i++)
            {
                if (model.CSInterestsViewModelList[i].isSelected)
                {
                    CSInterest ci = context!.CSInterests.Find(i + 1)!;
                    contentString += " " + ci.Name + ", ";
                }
            }

            for (int i = 0; i < model.HobbiesViewModelList!.Count; i++)
            {
                if (model.HobbiesViewModelList[i].isSelected)
                {
                    Hobby h = context!.Hobbies.Find(i + 1)!;
                    contentString += " " + h.Name + ", ";
                }
            }

            return Content(contentString!);
        }

    }
}
