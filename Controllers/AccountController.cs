using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CBApp.Models;
using CBApp.Data;
using System.Text.RegularExpressions;

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
        public AccountController(ApplicationDbContext _context, UserManager<User> _userManager,
            SignInManager<User> _signInManager)
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        [HttpGet]
        public IActionResult Register()
        {
            // Logic to clear error list if user refreshes the page (to not highlight fields with bad input in red)
            if (HttpContext.Session.GetInt32("RedirectToForm") != null)
            {
                // If this is not a redirect from unsuccessful POST method, then the errors list should be cleared out
                if (HttpContext.Session.GetInt32("RedirectToForm") != 1)
                {
                    // Clear the error list if action method is not being called due to redirect from HttpPost method because of bad input
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

            HttpContext.Session.SetInt32("RedirectToForm", 0);

            return View(model);
        }






        [HttpGet]
        public IActionResult RegisterInSteps()
        {
            // Logic to clear error list if user refreshes the page (to not highlight fields with bad input in red)
            if (HttpContext.Session.GetInt32("RedirectToForm") != null)
            {
                // If this is not a redirect from unsuccessful POST method, then the errors list should be cleared out
                if (HttpContext.Session.GetInt32("RedirectToForm") != 1)
                {
                    // Clear the error list if action method is not being called due to redirect from HttpPost method because of bad input
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

            HttpContext.Session.SetInt32("RedirectToForm", 0);

            return View(model);
        }













        [HttpPost]
        public async Task<IActionResult> RegisterInSteps(RegisterViewModel model)
        {

            // Create a new object storing values for different types of errors possible for the form input
            CreateUserErrors errorsObject = new CreateUserErrors();
            // Records number of errors in the user input
            int ErrorCounter = 0;

            // Find and record errors to store in session variable
            /********************************Store Errors related to SlackId***********************************************************/
            // Error: user did not enter a SlackId
            if (model!.SlackId == null)
            {
                // Set errorsObject property for there not being a SlackId (to generate corresponding error-message in the .cshtml view)
                errorsObject!.No_Slack_Id = true;
                ++ErrorCounter;
            }
            // User did enter a SlackId...
            else
            {
                // Error: SlackId entered is over 50 characters long
                if (model.SlackId!.Length < 5 || model.SlackId!.Length > 50)
                {
                    errorsObject!.Bad_Slack_Id_Length = true;
                    ++ErrorCounter;
                }

                // Attribution (for C# regex tutorial): https://www.techiedelight.com/check-string-consists-alphanumeric-characters-csharp/
                // Attribution (for checking if SlackId input string contains at least one number):
                // https://stackoverflow.com/questions/1540620/check-if-a-string-has-at-least-one-number-in-it-using-linq
                // Error: SlackId contains non-alphanumeric characters
                if (!Regex.IsMatch(model.SlackId!, "^[a-zA-Z0-9]*$")
                    || !model.SlackId.Any(char.IsDigit))
                {
                    errorsObject!.Invalid_Slack_Id = true;
                    ++ErrorCounter;
                }

            }
            /***************************************End of finding Errors related to SlackId**********************************************/



            /********************************Store Errors related to Username*************************************************************/
            if (model!.UserName == null)
            {
                // Error: no username entered
                errorsObject!.No_Username = true;
                ++ErrorCounter;
            }
            else
            {
                // Error: username is too long
                if (model.UserName!.Length < 6 || model.UserName!.Length > 70)
                {
                    errorsObject!.Bad_Username_Length = true;
                    ++ErrorCounter;
                }
                // Error: username contains characters that are neither alphanumeric or '_'
                if (!Regex.IsMatch(model.UserName!, "^[a-zA-Z0-9_]*$"))
                {
                    errorsObject!.Invalid_Username = true;
                    ++ErrorCounter;
                }

            }
            /********************************End of finding Errors related to Username****************************************************/


            /********************************Store Errors related to Password*************************************************************/
            if (model!.Password == null)
            {
                // Error: no password entered
                errorsObject!.No_Password = true;
                ++ErrorCounter;
            }
            else
            {
                // Error: password is under 8 characters
                if (model.Password.Length < 8)
                {
                    errorsObject!.Bad_Password_Length = true;
                    ++ErrorCounter;
                }
                // Error: password is not the same as confirm password
                if (model.Password != model.ConfirmPassword)
                {
                    errorsObject!.Passwords_Do_Not_Match = true;
                    ++ErrorCounter;
                }

            }
            /********************************End of finding Errors related to Password****************************************************/



            /********************************Stores Errors related to Dropdown Menus******************************************************/
            // No career phase id selected / career phase id is one that does not exist (below 1 and above 3, as there are only 3 options
            // Default before selection is 0 .: 0 = no option selected
            if (model!.SelectedCareerPhaseId < 1 || model!.SelectedCareerPhaseId > 3)
            {
                errorsObject!.No_Career_Phase_Selected = true;
                ++ErrorCounter;
            }

            // No experience level id selected or id is below 1 or above 5 (only five experience levels exist)
            // Default before selection is 0 .: 0 = no option selected
            if (model!.SelectedExperienceLevelId < 1 || model!.SelectedExperienceLevelId > 5)
            {
                errorsObject!.No_Experience_Level_Selected = true;
                ++ErrorCounter;
            }
            /********************************End of Errors related to Dropdown Menus******************************************************/



            /********************************Stores Errors related to Multiple Checkbox selections****************************************/
            // Error: no favourite programming languages selected
            int programmingLanguagesSelectedCounter = 0;
            foreach (var p_lang_view_model in model.ProgrammingLanguagesViewModelList!)
            {
                if (p_lang_view_model.isSelected)
                {
                    ++programmingLanguagesSelectedCounter;
                }
            }
            if (programmingLanguagesSelectedCounter == 0)
            {
                errorsObject.Wrong_Number_Programming_Languages_Selected = true;
                ++ErrorCounter;
            }

            // Error: no Computer Science interests selected
            int csInterestsSelectedCounter = 0;
            foreach (var interest in model.CSInterestsViewModelList!)
            {
                if (interest.isSelected)
                {
                    ++csInterestsSelectedCounter;
                }
            }
            if (csInterestsSelectedCounter == 0)
            {
                errorsObject.Wrong_Number_CSInterests_Selected = true;
                ++ErrorCounter;
            }

            // Error: wrong number of hobbies selected (less than 3 or more than 10)
            int hobbiesSelectedCounter = 0;
            foreach (var hobby in model.HobbiesViewModelList!)
            {
                if (hobby.isSelected)
                {
                    ++hobbiesSelectedCounter;
                }
            }
            if (hobbiesSelectedCounter < 3
                || hobbiesSelectedCounter > 10
            )
            {
                errorsObject.Wrong_Number_Hobbies_Selected = true;
                ++ErrorCounter;
            }
            /********************************End of Errors related to Multiple Checkbox selections****************************************/


            // If errors are found --> set them in the session object called "InputErrors"
            if (ErrorCounter > 0)
            {
                // Converts the errorsObject into JSON and stores it in the session using the session-extension SetObject method
                HttpContext.Session.SetObject("InputErrors", errorsObject);
                // Will have to redirect to the GET method for the form but include the error-messages this time
                // The 1 value in session means "true", as bools cannot be set in C# with HttpContext.Session
                // 1 means that the GET request for the same form will be a redirect from the POST method --> errors are not cleared in HttpGet version
                // of the method
                HttpContext.Session.SetInt32("RedirectToForm", 1);

                // For testing purposes --> return Json with the errors
                return RedirectToAction("Register");
            }

            if (ModelState.IsValid)
            {

                // Model is valid --> create the user and upload it to the database
                User user = new User
                {
                    SlackId = model.SlackId,
                    UserName = model.UserName,
                    CareerPhaseId = model.SelectedCareerPhaseId,
                    CareerPhase = context!.CareerPhases.Find(model.SelectedCareerPhaseId),
                    ExperienceLevelId = model.SelectedExperienceLevelId,
                    ExperienceLevel = context.ExperienceLevels.Find(model.SelectedExperienceLevelId),
                    GenderId = model.SelectedGenderId
                };


                // Update Gender "navigation property" of user only if Gender has been selected
                // We know if a gender has been selected if the Id chosen is not 0 (0 is the default value)
                if (model.SelectedGenderId != 0)
                {
                    user.Gender = context.Genders.Find(model.SelectedGenderId);
                }

                // Set user's natural langugaes to those selected if any (many-to-many field)
                user.NaturalLanguageUsers = new List<NaturalLanguageUser>();
                for (int i = 0; i < model.NaturalLanguagesViewModelList!.Count; ++i)
                {
                    if (model.NaturalLanguagesViewModelList[i].isSelected)
                    {
                        // Id of the language will always be the 'i' index + 1 (starts from 1 not 0)
                        NaturalLanguage nLang = context.NaturalLanguages.Find(i + 1)!;

                        // Create many-to-many relationship called "NaturalLanguageUser"
                        NaturalLanguageUser nlUser = new NaturalLanguageUser
                        {
                            NaturalLanguageId = i + 1,
                            SlackId = model.SlackId!,
                            User = user,
                            NaturalLanguage = nLang
                        };

                        // Add a NaturalLanguageUser to the ICollection in the User class
                        user.NaturalLanguageUsers.Add(nlUser);

                        // Add the many-to-many relationship to the context
                        context.NaturalLanguageUsers.Add(nlUser);
                    }
                }

                // Set user's favourite programming languages to those selected (many-to-many field)
                user.ProgrammingLanguageUsers = new List<ProgrammingLanguageUser>();
                for (int i = 0; i < model.ProgrammingLanguagesViewModelList.Count; ++i)
                {
                    if (model.ProgrammingLanguagesViewModelList[i].isSelected)
                    {

                        ProgrammingLanguage pLang = context.ProgrammingLanguages.Find(i + 1)!;

                        ProgrammingLanguageUser plUser = new ProgrammingLanguageUser
                        {
                            ProgrammingLanguageId = i + 1,
                            SlackId = model.SlackId!,
                            User = user,
                            ProgrammingLanguage = pLang
                        };


                        // Add a ProgrammingLanguageUser to the ICollection in the User class
                        user.ProgrammingLanguageUsers.Add(plUser);
                        // Add the many-to-many relationship to the context
                        context.ProgrammingLanguageUsers.Add(plUser);

                    }
                }

                // Set user's favourite Computer Science interests to those selected (many-to-many field)
                user.CSInterestUsers = new List<CSInterestUser>();
                for (int i = 0; i < model.CSInterestsViewModelList.Count; ++i)
                {
                    if (model.CSInterestsViewModelList[i].isSelected)
                    {

                        CSInterest interest = context.CSInterests.Find(i + 1)!;

                        CSInterestUser interestUser = new CSInterestUser
                        {
                            CSInterestId = i + 1,
                            SlackId = model.SlackId!,
                            User = user,
                            CSInterest = interest
                        };

                        user.CSInterestUsers.Add(interestUser);
                        context.CSInterestUsers.Add(interestUser);

                    }
                }

                // Set user's favourite hobbies to those selected (many-to-many field)
                user.HobbyUsers = new List<HobbyUser>();
                for (int i = 0; i < model.HobbiesViewModelList.Count; ++i)
                {
                    if (model.HobbiesViewModelList[i].isSelected)
                    {

                        Hobby hobby = context.Hobbies.Find(i + 1)!;

                        HobbyUser hobbyUser = new HobbyUser
                        {
                            HobbyId = i + 1,
                            SlackId = model.SlackId!,
                            User = user,
                            Hobby = hobby
                        };

                        user.HobbyUsers.Add(hobbyUser);
                        context.HobbyUsers.Add(hobbyUser);

                    }
                }


                var result = await userManager.CreateAsync(user, model.Password);

                // If the IdentityResult object is true, then sign the user in using a session cookie
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                // If create user operation fails, code adds errors to form
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    // Returns to the Registration page if the model is invalid - displays the errors in the form
                    return Content("Model is valid but could NOT create User!");
                }

            }

            return Content("MODEL INVALID");

        }











        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            // Create a new object storing values for different types of errors possible for the form input
            CreateUserErrors errorsObject = new CreateUserErrors();
            // Records number of errors in the user input
            int ErrorCounter = 0;

            // Find and record errors to store in session variable
            /********************************Store Errors related to SlackId***********************************************************/
            // Error: user did not enter a SlackId
            if (model!.SlackId == null)
            {
                // Set errorsObject property for there not being a SlackId (to generate corresponding error-message in the .cshtml view)
                errorsObject!.No_Slack_Id = true;
                ++ErrorCounter;
            }
            // User did enter a SlackId...
            else
            {
                // Error: SlackId entered is over 50 characters long
                if (model.SlackId!.Length < 5 || model.SlackId!.Length > 50)
                {
                    errorsObject!.Bad_Slack_Id_Length = true;
                    ++ErrorCounter;
                }

                // Attribution (for C# regex tutorial): https://www.techiedelight.com/check-string-consists-alphanumeric-characters-csharp/
                // Attribution (for checking if SlackId input string contains at least one number):
                // https://stackoverflow.com/questions/1540620/check-if-a-string-has-at-least-one-number-in-it-using-linq
                // Error: SlackId contains non-alphanumeric characters
                if (!Regex.IsMatch(model.SlackId!, "^[a-zA-Z0-9]*$")
                    || !model.SlackId.Any(char.IsDigit))
                {
                    errorsObject!.Invalid_Slack_Id = true;
                    ++ErrorCounter;
                }

            }
            /***************************************End of finding Errors related to SlackId**********************************************/



            /********************************Store Errors related to Username*************************************************************/
            if (model!.UserName == null)
            {
                // Error: no username entered
                errorsObject!.No_Username = true;
                ++ErrorCounter;
            }
            else
            {
                // Error: username is too long
                if (model.UserName!.Length < 6 || model.UserName!.Length > 70)
                {
                    errorsObject!.Bad_Username_Length = true;
                    ++ErrorCounter;
                }
                // Error: username contains characters that are neither alphanumeric or '_'
                if (!Regex.IsMatch(model.UserName!, "^[a-zA-Z0-9_]*$"))
                {
                    errorsObject!.Invalid_Username = true;
                    ++ErrorCounter;
                }

            }
            /********************************End of finding Errors related to Username****************************************************/


            /********************************Store Errors related to Password*************************************************************/
            if (model!.Password == null)
            {
                // Error: no password entered
                errorsObject!.No_Password = true;
                ++ErrorCounter;
            }
            else
            {
                // Error: password is under 8 characters
                if (model.Password.Length < 8)
                {
                    errorsObject!.Bad_Password_Length = true;
                    ++ErrorCounter;
                }
                // Error: password is not the same as confirm password
                if (model.Password != model.ConfirmPassword)
                {
                    errorsObject!.Passwords_Do_Not_Match = true;
                    ++ErrorCounter;
                }

            }
            /********************************End of finding Errors related to Password****************************************************/



            /********************************Stores Errors related to Dropdown Menus******************************************************/
            // No career phase id selected / career phase id is one that does not exist (below 1 and above 3, as there are only 3 options
            // Default before selection is 0 .: 0 = no option selected
            if (model!.SelectedCareerPhaseId < 1 || model!.SelectedCareerPhaseId > 3)
            {
                errorsObject!.No_Career_Phase_Selected = true;
                ++ErrorCounter;
            }

            // No experience level id selected or id is below 1 or above 5 (only five experience levels exist)
            // Default before selection is 0 .: 0 = no option selected
            if (model!.SelectedExperienceLevelId < 1 || model!.SelectedExperienceLevelId > 5)
            {
                errorsObject!.No_Experience_Level_Selected = true;
                ++ErrorCounter;
            }
            /********************************End of Errors related to Dropdown Menus******************************************************/



            /********************************Stores Errors related to Multiple Checkbox selections****************************************/
            // Error: no favourite programming languages selected
            int programmingLanguagesSelectedCounter = 0;
            foreach (var p_lang_view_model in model.ProgrammingLanguagesViewModelList!)
            {
                if (p_lang_view_model.isSelected)
                {
                    ++programmingLanguagesSelectedCounter;
                }
            }
            if (programmingLanguagesSelectedCounter == 0)
            {
                errorsObject.Wrong_Number_Programming_Languages_Selected = true;
                ++ErrorCounter;
            }

            // Error: no Computer Science interests selected
            int csInterestsSelectedCounter = 0;
            foreach (var interest in model.CSInterestsViewModelList!)
            {
                if (interest.isSelected)
                {
                    ++csInterestsSelectedCounter;
                }
            }
            if (csInterestsSelectedCounter == 0)
            {
                errorsObject.Wrong_Number_CSInterests_Selected = true;
                ++ErrorCounter;
            }

            // Error: wrong number of hobbies selected (less than 3 or more than 10)
            int hobbiesSelectedCounter = 0;
            foreach (var hobby in model.HobbiesViewModelList!)
            {
                if (hobby.isSelected)
                {
                    ++hobbiesSelectedCounter;
                }
            }
            if (hobbiesSelectedCounter < 3
                || hobbiesSelectedCounter > 10
            )
            {
                errorsObject.Wrong_Number_Hobbies_Selected = true;
                ++ErrorCounter;
            }
            /********************************End of Errors related to Multiple Checkbox selections****************************************/


            // If errors are found --> set them in the session object called "InputErrors"
            if (ErrorCounter > 0)
            {
                // Converts the errorsObject into JSON and stores it in the session using the session-extension SetObject method
                HttpContext.Session.SetObject("InputErrors", errorsObject);
                // Will have to redirect to the GET method for the form but include the error-messages this time
                // The 1 value in session means "true", as bools cannot be set in C# with HttpContext.Session
                // 1 means that the GET request for the same form will be a redirect from the POST method --> errors are not cleared in HttpGet version
                // of the method
                HttpContext.Session.SetInt32("RedirectToForm", 1);

                // For testing purposes --> return Json with the errors
                return RedirectToAction("Register");
            }

            if (ModelState.IsValid)
            {

                // Model is valid --> create the user and upload it to the database
                User user = new User {
                    SlackId = model.SlackId,
                    UserName = model.UserName,
                    CareerPhaseId = model.SelectedCareerPhaseId,
                    CareerPhase = context!.CareerPhases.Find(model.SelectedCareerPhaseId),
                    ExperienceLevelId = model.SelectedExperienceLevelId,
                    ExperienceLevel = context.ExperienceLevels.Find(model.SelectedExperienceLevelId),
                    GenderId = model.SelectedGenderId
                };


                // Update Gender "navigation property" of user only if Gender has been selected
                // We know if a gender has been selected if the Id chosen is not 0 (0 is the default value)
                if (model.SelectedGenderId != 0)
                {
                    user.Gender = context.Genders.Find(model.SelectedGenderId);
                }

                // Set user's natural langugaes to those selected if any (many-to-many field)
                user.NaturalLanguageUsers = new List<NaturalLanguageUser>();
                for (int i = 0; i < model.NaturalLanguagesViewModelList!.Count; ++i)
                {
                    if (model.NaturalLanguagesViewModelList[i].isSelected)
                    {
                        // Id of the language will always be the 'i' index + 1 (starts from 1 not 0)
                        NaturalLanguage nLang = context.NaturalLanguages.Find(i + 1)!;

                        // Create many-to-many relationship called "NaturalLanguageUser"
                        NaturalLanguageUser nlUser = new NaturalLanguageUser
                        {
                            NaturalLanguageId = i + 1,
                            SlackId = model.SlackId!,
                            User = user,
                            NaturalLanguage = nLang
                        };

                        // Add a NaturalLanguageUser to the ICollection in the User class
                        user.NaturalLanguageUsers.Add(nlUser);

                        // Add the many-to-many relationship to the context
                        context.NaturalLanguageUsers.Add(nlUser);
                    }
                }

                // Set user's favourite programming languages to those selected (many-to-many field)
                user.ProgrammingLanguageUsers = new List<ProgrammingLanguageUser>();
                for (int i = 0; i < model.ProgrammingLanguagesViewModelList.Count; ++i)
                {
                    if (model.ProgrammingLanguagesViewModelList[i].isSelected)
                    {

                        ProgrammingLanguage pLang = context.ProgrammingLanguages.Find(i + 1)!;

                        ProgrammingLanguageUser plUser = new ProgrammingLanguageUser
                        {
                            ProgrammingLanguageId = i + 1,
                            SlackId = model.SlackId!,
                            User = user,
                            ProgrammingLanguage = pLang
                        };


                        // Add a ProgrammingLanguageUser to the ICollection in the User class
                        user.ProgrammingLanguageUsers.Add(plUser);
                        // Add the many-to-many relationship to the context
                        context.ProgrammingLanguageUsers.Add(plUser);

                    }
                }

                // Set user's favourite Computer Science interests to those selected (many-to-many field)
                user.CSInterestUsers = new List<CSInterestUser>();
                for (int i = 0; i < model.CSInterestsViewModelList.Count; ++i)
                {
                    if (model.CSInterestsViewModelList[i].isSelected)
                    {

                        CSInterest interest = context.CSInterests.Find(i + 1)!;

                        CSInterestUser interestUser = new CSInterestUser
                        {
                            CSInterestId = i + 1,
                            SlackId = model.SlackId!,
                            User = user,
                            CSInterest = interest
                        };

                        user.CSInterestUsers.Add(interestUser);
                        context.CSInterestUsers.Add(interestUser);

                    }
                }

                // Set user's favourite hobbies to those selected (many-to-many field)
                user.HobbyUsers = new List<HobbyUser>();
                for (int i = 0; i < model.HobbiesViewModelList.Count; ++i)
                {
                    if (model.HobbiesViewModelList[i].isSelected)
                    {

                        Hobby hobby = context.Hobbies.Find(i + 1)!;

                        HobbyUser hobbyUser = new HobbyUser
                        {
                            HobbyId = i + 1,
                            SlackId = model.SlackId!,
                            User = user,
                            Hobby = hobby
                        };

                        user.HobbyUsers.Add(hobbyUser);
                        context.HobbyUsers.Add(hobbyUser);

                    }
                }


                var result = await userManager.CreateAsync(user, model.Password);

                // If the IdentityResult object is true, then sign the user in using a session cookie
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                // If create user operation fails, code adds errors to form
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    // Returns to the Registration page if the model is invalid - displays the errors in the form
                    return Content("Model is valid but could NOT create User!");
                }

            }

            return Content("MODEL INVALID");

        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login(string returnURL = "")
        {
            // Initialize the ReturnUrl property of the view model to the parameter of the method
            // This works because the name of the parameter matches the name of the query string parameter
            // (Murach book, p. 676)
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Username, model.Password, isPersistent: model.RememberMe,
                    lockoutOnFailure: false);

                // If result succeeded, check if ReturnUrl property of view model exists
                if (result.Succeeded)
                {
                    // Checking that the ReturnUrl contains a local URL protects against hacker redirecting
                    // the browser to an external malicious website
                    if (!string.IsNullOrEmpty(model.ReturnUrl)
                        && Url.IsLocalUrl(model.ReturnUrl)
                    )
                    {
                        // Redirect to the Url specified by ReturnUrl view model property
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        // Otherwise, return to homepage
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }
    }

}
