using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBApp.Models;
using CBApp.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.Xml;
using Azure.Core;

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

            // Logic to clear error list if user refreshes the page (to not highlight fields with bad input in red)
            if (HttpContext.Session.GetInt32("FormRedirect") != null)
            {
                if (HttpContext.Session.GetInt32("FormRedirect") != 1)
                {
                    // Clear the error list if action method is not being called due to redirect from HttpPost method (bad input)
                    if (HttpContext.Session.GetObject<CreateUserErrors>("errors") != null)
                    {
                        CreateUserErrors emptyErrorList = new CreateUserErrors();
                        HttpContext.Session.SetObject("errors", emptyErrorList);
                    }
                }
            }

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

            HttpContext.Session.SetInt32("isRedirect", 0);

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> CreateUserTest(CreateUserViewModel model)
        {

            if (model.NaturalLanguagesViewModelList == null || model.CSInterestsViewModelList == null)
            {
                return Content("Everything is fucking null ONE.");
            }

            // Create a new object storing types of errors for the form input
            CreateUserErrors errorsObject = new CreateUserErrors();

            int ErrorCounter = 0;

            // CODE FOR SLACK_ID ERRORS IN THE INPUT FORM
            // Model cannot be null in this httppost method --> use null forgiving '!' operator
            if (model!.user!.SlackId == null) // User did not enter SlackId
            {
                errorsObject!.No_Slack_Id = true;
                ++ErrorCounter;
            }
            else
            {
                // SlackId fields is not null --> do other checks for right format
                if (model.user.SlackId!.Length > 50)
                {
                    errorsObject!.Slack_Id_Too_Long = true;
                    ++ErrorCounter;
                }

                // Attribution (for C# regex tutorial): https://www.techiedelight.com/check-string-consists-alphanumeric-characters-csharp/
                // Attribution (for checking if SlackId input string contains at least one number):
                // https://stackoverflow.com/questions/1540620/check-if-a-string-has-at-least-one-number-in-it-using-linq
                if (!Regex.IsMatch(model.user.SlackId!, "^[a-zA-Z0-9]*$")
                    || !model.user.SlackId.Any(char.IsDigit))
                {
                    errorsObject!.Invalid_Slack_Id = true;
                    ++ErrorCounter;
                }

            }

            // CODE FOR USERNAME ERRORS IN THE INPUT FORM
            if (model!.user!.UserName == null) // User did not enter SlackId
            {
                errorsObject!.No_Username = true;
                ++ErrorCounter;
            }
            else
            {
                // SlackId fields is not null --> do other checks for right format
                if (model.user.UserName!.Length > 70)
                {
                    errorsObject!.Username_Too_Long = true;
                    ++ErrorCounter;
                }

                // Attribution (for C# regex tutorial): https://www.techiedelight.com/check-string-consists-alphanumeric-characters-csharp/
                // Attribution (for checking if SlackId input string contains at least one number):
                // https://stackoverflow.com/questions/1540620/check-if-a-string-has-at-least-one-number-in-it-using-linq
                if (!Regex.IsMatch(model.user.UserName!, "^[a-zA-Z0-9_]*$"))
                {
                    errorsObject!.Invalid_Username = true;
                    ++ErrorCounter;
                }

            }

            // CODE FOR CHECKING IF USER HAS SELECTED CAREER PHASE OPTION
            if (model!.SelectedCareerPhaseId == null || model!.SelectedCareerPhaseId < 1 || model!.SelectedCareerPhaseId > 3)
            {
                errorsObject!.No_Career_Phase_Selected = true;
                ++ErrorCounter;
            }

            // CODE FOR CHECKING IF USER HAS SELECTED EXPERIENCE LEVEL
            if (model!.SelectedExperienceLevelId == null || model!.SelectedExperienceLevelId < 1 || model!.SelectedExperienceLevelId > 3)
            {
                errorsObject!.No_Experience_Level_Selected = true;
                ++ErrorCounter;
            }

            // CODE FOR CHECKING AT LEAST ONE PROGRAMMING LANGUAGE IS SELECTED
            int programmingLanguagesSelectedCounter = 0;
            foreach (var p_lang_view_model in model.ProgrammingLanguagesViewModelList!)
            {
                if (p_lang_view_model.isSelected)
                {
                    ++programmingLanguagesSelectedCounter;
                }
            }
            // Store the error if user has not selected any programming languages
            if (programmingLanguagesSelectedCounter == 0)
            {
                errorsObject.Wrong_Number_Programming_Languages_Selected = true;
                ++ErrorCounter;
            }

            // CODE FOR CHECKING AT LEAST ONE CS INTEREST IS SELECTED
            int csInterestsSelectedCounter = 0;
            foreach (var interest in model.CSInterestsViewModelList!)
            {
                if (interest.isSelected)
                {
                    ++csInterestsSelectedCounter;
                }
            }
            // Store the error if user has not selected any programming languages
            if (csInterestsSelectedCounter == 0)
            {
                errorsObject.Wrong_Number_CSInterests_Selected = true;
                ++ErrorCounter;
            }

            // CODE FOR CHECKING THAT BETWEEN 3 - 10 HOBBIES ARE SELECTED
            int hobbiesSelectedCounter = 0;
            foreach (var hobby in model.HobbiesViewModelList!)
            {
                if (hobby.isSelected)
                {
                    ++hobbiesSelectedCounter;
                }
            }
            // Store the error if user has not selected any programming languages
            if (hobbiesSelectedCounter < 3 
                || hobbiesSelectedCounter > 10
            )
            {
                errorsObject.Wrong_Number_Hobbies_Selected = true;
                ++ErrorCounter;
            }

            if (model.NaturalLanguagesViewModelList == null)
            {
                return Content("Everything is fucking null.");
            }
            //else
            //{
            //    int counter = model.NaturalLanguagesViewModelList.Count;
            //    return Content(counter.ToString());
            //}



            // Redirects to original form (with errors stored in the session) if the error counter reports errors
            if (ErrorCounter > 0)
            {
                // Converts errors object back into JSON and stores it back in the session
                //string errorsString = JsonConvert.SerializeObject(errorsObject); --> old code before extension of Session class
                HttpContext.Session.SetObject("errors", errorsObject);
                // Make it clear using session that the form page is a redirect (to display the errors in session)
                HttpContext.Session.SetInt32("FormRedirect", 1); // 1 = true, means the form is resent using redirect to Create action
                return RedirectToAction("CreateUserTest");
            }
             

            // For testing purposes: return Content view result instead of RedirectToAction if all fields have been properly filled in by the user
            if (ModelState.IsValid)
            {
                User user = new User();
                user.SlackId = model!.user!.SlackId;
                user.UserName = model!.user!.UserName;
                user.CareerPhaseId = model.SelectedCareerPhaseId;
                user.CareerPhase = context.CareerPhases.Find(model.SelectedCareerPhaseId);
                user.ExperienceLevelId = model.SelectedExperienceLevelId;
                user.ExperienceLevel = context.ExperienceLevels.Find(model.SelectedExperienceLevelId);

                if (model.SelectedGenderId != 0)
                {
                    user.GenderId = model.SelectedGenderId;
                    user.Gender = context.Genders.Find(model.SelectedGenderId);
                }


                user.NaturalLanguageUsers = new List<NaturalLanguageUser>();
                for (int i = 0; i < model.NaturalLanguagesViewModelList.Count; ++i)
                {
                    if (model.NaturalLanguagesViewModelList[i].isSelected)
                    {

                        NaturalLanguage nLang = context.NaturalLanguages.Find(i + 1)!;

                        NaturalLanguageUser nlUser = new NaturalLanguageUser
                        {
                            NaturalLanguageId = i + 1,
                            SlackId = model.user.SlackId!,
                            User = user,
                            NaturalLanguage = nLang
                        };

                        user.NaturalLanguageUsers.Add(nlUser);

                        context.NaturalLanguageUsers.Add(nlUser);

                    }
                }

                user.ProgrammingLanguageUsers = new List<ProgrammingLanguageUser>();
                for (int i = 0; i < model.ProgrammingLanguagesViewModelList.Count; ++i)
                {
                    if (model.ProgrammingLanguagesViewModelList[i].isSelected)
                    {

                        ProgrammingLanguage pLang = context.ProgrammingLanguages.Find(i + 1)!;

                        ProgrammingLanguageUser plUser = new ProgrammingLanguageUser
                        {
                            ProgrammingLanguageId = i + 1,
                            SlackId = model.user.SlackId!,
                            User = user,
                            ProgrammingLanguage = pLang
                        };

                        user.ProgrammingLanguageUsers.Add(plUser);
                        context.ProgrammingLanguageUsers.Add(plUser);

                    }
                }


                user.CSInterestUsers = new List<CSInterestUser>();

                for (int i = 0; i < model.CSInterestsViewModelList.Count; ++i)
                {
                    if (model.CSInterestsViewModelList[i].isSelected)
                    {

                        CSInterest interest = context.CSInterests.Find(i + 1)!;

                        CSInterestUser interestUser = new CSInterestUser
                        {
                            CSInterestId = i + 1,
                            SlackId = model.user.SlackId!,
                            User = user,
                            CSInterest = interest
                        };

                        user.CSInterestUsers.Add(interestUser);
                        context.CSInterestUsers.Add(interestUser);

                    }
                }


                user.HobbyUsers = new List<HobbyUser>();
                for (int i = 0; i < model.HobbiesViewModelList.Count; ++i)
                {
                    if (model.HobbiesViewModelList[i].isSelected)
                    {

                        Hobby hobby = context.Hobbies.Find(i + 1)!;

                        HobbyUser hobbyUser = new HobbyUser
                        {
                            HobbyId = i + 1,
                            SlackId = model.user.SlackId!,
                            User = user,
                            Hobby = hobby
                        };

                        user.HobbyUsers.Add(hobbyUser);
                        context.HobbyUsers.Add(hobbyUser);

                    }
                }



                context.Add(user);
                await context.SaveChangesAsync();

                return View("SuccessfullyCreatedUser");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                return Content(errors.ToString());
                //return RedirectToAction("CreateUserTest");
            }

        }


    }
}