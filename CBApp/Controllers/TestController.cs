using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBApp.Models;
using CBApp.Data;

namespace CBApp.Controllers
{
    public class TestController : Controller
    {
        private ApplicationDbContext context;


        /** Provides the Database Context object in it’s constructor’s parameter, this will provide the controller
            with the Database Context object through Dependency Injection.
        */
        public TestController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Content("Index");
        }

        [HttpGet]
        public IActionResult UpdateQuestion(int id)
        {
            var qc = context.Questions.Where(q => q.QuestionId == id).FirstOrDefault();
            return View(qc);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuestion(Question question)
        {

            if (ModelState.IsValid)
            {

                if (question.QuestionId == 0)
                {
                    return Content("Is 0");
                }
                else
                {
                    context.Update(question);
                    await context.SaveChangesAsync();
                    return View(question);
                }
            }
            else
            {
                return View(question);
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var vq = context.Questions.Where(q => q.QuestionId == id).FirstOrDefault();
            context.Remove(vq);
            await context.SaveChangesAsync();
            return RedirectToAction("ReadQuestions");
        }


        [HttpGet]
        public IActionResult CreateQuestion()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                context.Add(question);
                await context.SaveChangesAsync();
                return RedirectToAction("ReadQuestions");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult ReadQuestions()
        {
            var questions = context.Questions.ToList();
            return View(questions);
        }

        //////// Testing user creation ///////////////
        [HttpGet]
        public IActionResult SelectProgrammingLanguages()
        {
            List<ProgrammingLanguage> programmingLangs = context.ProgrammingLanguages.ToList();

            List<ProgrammingLanguageViewModel> programmingLangsViewModelList = new List<ProgrammingLanguageViewModel>();

            foreach(var language in programmingLangs)
            {
                programmingLangsViewModelList.Add(
                    new ProgrammingLanguageViewModel
                    {
                        programmingLanguage = language,
                        isSelected = false
                    }
                );
            }

            return View(programmingLangsViewModelList);
        }

        [HttpPost]
        public IActionResult SelectProgrammingLanguages(List<ProgrammingLanguageViewModel> programmingLangs)
        {
            if (programmingLangs.Count(p => p.isSelected) == 0)
            {
                return Content("You have not selected anything.");
            }
            else
            {
                List < ProgrammingLanguage > selectedLangs = new List<ProgrammingLanguage>();
                foreach (var language in programmingLangs)
                {
                    if (language.isSelected)
                    {
                        selectedLangs.Add(language.programmingLanguage);
                    }
                }
                return Content(selectedLangs[0].Name);
            }
        }

        [HttpGet]
        public IActionResult CreateUserTest()
        {
            CreateUserViewModel model = new CreateUserViewModel();
            
            // Populate view model with CareerPhases
            model.careerPhaseViewModelList = new List<CareerPhaseViewModel>();

            List<CareerPhase> careerPhases = context.CareerPhases.ToList();

            foreach (var cp in careerPhases)
            {
                model.careerPhaseViewModelList.Add(
                    new CareerPhaseViewModel
                    {
                        careerPhase = cp,
                        isSelected = false
                    }
                );
            }

            // Populate view model with ExperienceLevels
            model.experienceLevelViewModelList = new List<ExperienceLevelViewModel>();

            List<ExperienceLevel> experienceLevels = context.ExperienceLevels.ToList();

            foreach (var el in experienceLevels)
            {
                model.experienceLevelViewModelList.Add(
                    new ExperienceLevelViewModel
                    {
                        experienceLevel = el,
                        isSelected = false
                    }
                );
            }


            // Populate view model with Genders
            model.genderViewModelList = new List<GenderViewModel>();

            List<Gender> genders = context.Genders.ToList();

            foreach (var g in genders)
            { 
                model.genderViewModelList.Add(
                    new GenderViewModel
                    {
                        gender = g,
                        isSelected = false
                    }
                );
            }

            // Populate view model with languages
            model.naturalLanguagesViewModelList = new List<NaturalLanguageViewModel>();

            List<NaturalLanguage> nlangs = context.NaturalLanguages.ToList();

            foreach (var nl in nlangs)
            {
                model.naturalLanguagesViewModelList.Add(
                    new NaturalLanguageViewModel
                    {
                        naturalLanguage = nl,
                        isSelected = false
                    }
                );
            }

            // Populate view model with programming languages
            model.programmingLanguagesViewModelList = new List<ProgrammingLanguageViewModel>();

            List<ProgrammingLanguage> plangs = context.ProgrammingLanguages.ToList();

            foreach (var pl in plangs)
            {
                model.programmingLanguagesViewModelList.Add(
                    new ProgrammingLanguageViewModel
                    {
                        programmingLanguage = pl,
                        isSelected = false
                    }
                );
            }


            // Populate view model with CS interests
            model.csInterestViewModelList = new List<CSInterestViewModel>();

            List<CSInterest> csInterests = context.CSInterests.ToList();

            foreach (var ci in csInterests)
            {
                model.csInterestViewModelList.Add(
                    new CSInterestViewModel
                    {
                        csInterest= ci,
                        isSelected = false
                    }
                );
            }

            // Populate view model with hobbies
            model.hobbiesViewModelList = new List<HobbyViewModel>();

            List<Hobby> hobbies = context.Hobbies.ToList();

            foreach (var h in hobbies)
            {
                model.hobbiesViewModelList.Add(
                    new HobbyViewModel
                    {
                        hobby = h,
                        isSelected = false
                    }
                );
            }

            return View(model);


        }

        [HttpPost]
        public IActionResult CreateUserTest(CreateUserViewModel model)
        {

            var userInput = model.user.SlackId.ToString() + ", " +
                model.user.UserName + ", " + model.user.Bio;

            if (ModelState.IsValid)
            {
                return Content("It works");
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();

                return Content(errors.ToString());
            }

        }


    }
}
