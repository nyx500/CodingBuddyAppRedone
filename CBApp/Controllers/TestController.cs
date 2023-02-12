using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBApp.Models;
using CBApp.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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

            foreach (var language in programmingLangs)
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
                List<ProgrammingLanguage> selectedLangs = new List<ProgrammingLanguage>();
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

            //if (HttpContext.Session.GetString("errors") == null)
            //{
            //    CreateUserErrors create_user_errors = new CreateUserErrors();
            //    string create_user_errors_Json = JsonConvert.SerializeObject(create_user_errors);
            //    HttpContext.Session.SetString("errors", create_user_errors_Json);
            //}

            return View(model);


        }

        [HttpPost]
        public IActionResult CreateUserTest(CreateUserViewModel model)
        {   
            // Create a new object storing types of errors for the form input
            CreateUserErrors errors_object = new CreateUserErrors();

            int ErrorCounter = 0;

            // CODE FOR SLACK_ID ERRORS IN THE INPUT FORM
            // Model cannot be null in this httppost method --> use null forgiving '!' operator
            if (model!.user!.SlackId == null) // User did not enter SlackId
            {
                errors_object!.No_Slack_Id = true;
                ++ErrorCounter;
            }
            else
            {
                // SlackId fields is not null --> do other checks for right format
                if (model.user.SlackId!.Length > 50)
                {
                    errors_object!.Slack_Id_Too_Long = true;
                    ++ErrorCounter;
                }

                // Attribution (for C# regex tutorial): https://www.techiedelight.com/check-string-consists-alphanumeric-characters-csharp/
                // Attribution (for checking if SlackId input string contains at least one number):
                // https://stackoverflow.com/questions/1540620/check-if-a-string-has-at-least-one-number-in-it-using-linq
                if (!Regex.IsMatch(model.user.SlackId!, "^[a-zA-Z0-9]*$")
                    || !model.user.SlackId.Any(char.IsDigit))
                {
                    errors_object!.Invalid_Slack_Id = true;
                    ++ErrorCounter;
                }

            }

            // CODE FOR USERNAME ERRORS IN THE INPUT FORM
            if (model!.user!.UserName == null) // User did not enter SlackId
            {
                errors_object!.No_Username = true;
                ++ErrorCounter;
            }
            else
            {
                // SlackId fields is not null --> do other checks for right format
                if (model.user.UserName!.Length > 70)
                {
                    errors_object!.Username_Too_Long = true;
                    ++ErrorCounter;
                }

                // Attribution (for C# regex tutorial): https://www.techiedelight.com/check-string-consists-alphanumeric-characters-csharp/
                // Attribution (for checking if SlackId input string contains at least one number):
                // https://stackoverflow.com/questions/1540620/check-if-a-string-has-at-least-one-number-in-it-using-linq
                if (!Regex.IsMatch(model.user.UserName!, "^[a-zA-Z0-9_]*$"))
                {
                    errors_object!.Invalid_Username = true;
                    ++ErrorCounter;
                }

            }

            // CODE FOR CHECKING IF USER HAS SELECTED CAREER PHASE OPTION
            if (model!.SelectedCareerPhaseId == null)
            {
                errors_object!.No_Career_Phase_Selected = true;
                ++ErrorCounter;
            }

            // CODE FOR CHECKING IF USER HAS SELECTED EXPERIENCE LEVEL
            if (model!.SelectedExperienceLevelId == null)
            {
                errors_object!.No_Experience_Level_Selected = true;
                ++ErrorCounter;
            }


            // Converts errors object back into JSON and stores it back in the session
            string errors_string = JsonConvert.SerializeObject(errors_object);
            HttpContext.Session.SetString("errors", errors_string);


            // Redirects to original form (with errors stored in the session) if the error counter reports errors
            if (ErrorCounter > 0)
            {
                return RedirectToAction("CreateUserTest");
            }


            if (ModelState.IsValid)
            {
                return Content("required fields are not null!");
            }
            else
            {
                //var errors = ModelState.Values.SelectMany(v => v.Errors);

                //return Content(errors.ToString());
                return RedirectToAction("CreateUserTest");
            }

        }


    }
}