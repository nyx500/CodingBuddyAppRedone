﻿using Microsoft.AspNetCore.Mvc;
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
    public class AccountController : Controller
    {
        // Needed for file uploads
        // Attribution: https://www.aspsnippets.com/questions/130814/Upload-Form-data-and-File-in-ASPNet-Core-using-jQuery-Ajax/
        private IWebHostEnvironment Environment;


        /**
         * Provides the Database Context object passed in through constructor’s parameter,
         * this will provide the controller with the Database Context object through a
         * Dependency Injection.
      */
        private ApplicationDbContext? context;
        public AccountController(ApplicationDbContext _context, UserManager<User> _userManager,
            SignInManager<User> _signInManager, IWebHostEnvironment _environment)
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
            Environment = _environment;
        }

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

      
        [HttpGet]
        public IActionResult RegisterInSteps()
        {
            // Logic to clear error list if user refreshes the page (to not highlight fields with bad input in red)
            if (HttpContext.Session.GetInt32("RedirectToRegistrationForm") != null)
            {
                // If this is not a redirect from unsuccessful POST method, then the errors list should be cleared out
                if (HttpContext.Session.GetInt32("RedirectToRegistrationForm") != 1)
                {
                    // Clear the error list if action method is not being called due to redirect from HttpPost method because of bad input
                    if (HttpContext.Session.GetObject<FormErrors>("InputErrors") != null)
                    {
                        FormErrors emptyErrorList = new FormErrors();
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
                );
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


            HttpContext.Session.SetInt32("RedirectToRegistrationForm", 0);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterInSteps(RegisterViewModel model)
        {

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
                    GenderId = 0
                };


                // Update Gender "navigation property" of user only if Gender has been selected
                // We know if a gender has been selected if the Id chosen is not 0 (0 is the default value)
                if (model.SelectedGenderId != 0)
                {
                    user.GenderId = model.SelectedGenderId;
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
                            NaturalLanguageId = (i + 1),
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

                // Adds the new user to the database
                context.Users.Add(user);
                var result = await userManager.CreateAsync(user, model.Password);

                // If the IdentityResult object is true, then sign the user in using a session cookie
                if (result.Succeeded)
                {
                    context.SaveChanges();

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                // If create user operation fails, code adds errors to form
                else
                {
                    var errors = result.Errors;
                    var message = string.Join(", ", errors);
                    ModelState.AddModelError("", message);

                    // Returns to the Registration page if the model is invalid - displays the errors in the form
                    return View(model);
                }

            }

            var messages = string.Join(" | ", ModelState.Values
              .SelectMany(v => v.Errors)
              .Select(e => e.ErrorMessage));
                    return Content(messages);

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
                User user = userManager.Users.Where(u => u.UserName == model.Username).FirstOrDefault<User>()!;

                // Do not let "deactivated" users in
                if (user.DeactivateRequest == true)
                {
                    ModelState.AddModelError("invalidLogin", "Invalid username.");
                    return View(model);
                }
                else
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

                ModelState.AddModelError("invalidLogin", "Invalid username/password combination!");
                return View(model);
            }

            return View(model);
        }



        [HttpPost]
        public JsonResult CheckUsername(string username = "")
        {
            List<User> users = userManager.Users.ToList();

            var result = false;

            foreach(User user in users)
            {
                if (user.UserName == username)
                {
                    result = true;
                }
            }

            return Json(result);
        }


        [HttpPost]
        public JsonResult CheckSlackId(string slackId = "")
        {
            List<User> users = userManager.Users.ToList();

            var result = false;

            foreach (User user in users)
            {
                if (user.SlackId == slackId)
                {
                    result = true;
                }
            }

            return Json(result);
        }

        [HttpGet]
        public ActionResult ViewProfile(string id = "")
        {
            return Content("SlackID: " + id.ToString());
        }

        /** Make this route only accessible if user is logged in! */
        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            // Get the current user
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;
            EditUserViewModel model = new EditUserViewModel();
            model.UserName = user.UserName;
            model.SlackId = user.SlackId;
            model.CareerPhase = user.CareerPhase;
            model.ExperienceLevel = user.ExperienceLevel;
            model.LanguageNames = new List<string>();
            model.QuestionAnswerBlocks = new List<QuestionAnswerBlock>();
            model.Bio = user.Bio;

            foreach (QuestionAnswerBlock qaBlock in user.QuestionAnswerBlocks)
            {
                model.QuestionAnswerBlocks.Add(qaBlock);
            }

            // Dropdown configuration to change Career Phase/Experience Level Options
            // Populate the view model with CareerPhases
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
            // Populate the view model with ExperienceLevels
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

            // Get user preferences
            model.LanguageNames = new List<String>();
            foreach (NaturalLanguageUser n in user.NaturalLanguageUsers)
            {
                string languageName = n.NaturalLanguage.Name;
                model.LanguageNames.Add(languageName);
            }

            model.ProgrammingLanguageNames = new List<String>();
            foreach (ProgrammingLanguageUser p in user.ProgrammingLanguageUsers)
            {
                string languageName = p.ProgrammingLanguage.Name;
                model.ProgrammingLanguageNames.Add(languageName);
            }

            model.CSInterestNames = new List<String>();
            foreach (CSInterestUser c in user.CSInterestUsers)
            {
                string interest = c.CSInterest.Name;
                model.CSInterestNames.Add(interest);
            }

            model.HobbyNames = new List<String>();
            foreach (HobbyUser h in user.HobbyUsers)
            {
                string hobbyName = h.Hobby.Name;
                model.HobbyNames.Add(hobbyName);
            }

            // Configure lists of options
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

            if (user.PictureFormat != null)
            {
                // Attribution: converting bytes to image (https://stackoverflow.com/questions/17952514/asp-net-mvc-how-to-display-a-byte-array-image-from-model)
                // Get the mime type
                string picFormat = user.PictureFormat!.Substring(1);
                string mimeType = "image/" + picFormat;
                string base64 = Convert.ToBase64String(user.Picture);
                model.ImageSrc = string.Format("data:{0};base64,{1}", mimeType, base64);
            }
            else
            {
                model.ImageSrc = null;
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public JsonResult UploadPicture(IFormFile file)
        {   
            // Validate file exists
            if (file != null)
            {   
                // Validate file is not empty
                if (file.Length > 0)
                {
                    // Validation Attribution: https://stackoverflow.com/questions/11063900/determine-if-uploaded-file-is-image-any-format-on-mvc
                    // Checks if the file uploaded is really an image
                    if (file.ContentType.ToLower() != "image/jpg" &&
                        file.ContentType.ToLower() !=  "image/png" &&
                        file.ContentType.ToLower() != "image/x-png" &&
                        file.ContentType.ToLower() != "image/jpeg")
                    {
                        return Json(new { error = "Bad image type!" });
                    }

                    if (Path.GetExtension(file.FileName).ToLower() != ".jpg"
                        && Path.GetExtension(file.FileName).ToLower() != ".png"
                        && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
                    {
                        return Json(new { error = "Bad image extension!" });
                    }

                    // Attribution: how to upload files to database https://tutexchange.com/how-to-upload-files-and-save-in-database-in-asp-net-core-mvc/
                    // Getting FileName
                    var fileName = Path.GetFileName(file.FileName);
                    // Get file extension
                    string fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    // Find the currently-logged in user by username
                    var username = User.Identity!.Name;
                    User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

                    user.PictureFormat = fileExtension;

                    using (var target = new MemoryStream()) {
                        file.CopyTo(target);
                        user.Picture = target.ToArray();
                    }

                    context.SaveChanges();

                    return Json(new { success = "New file selected!", didItWork = user.Picture.ToString() });
                }

            }

            return Json(new { error = "No file chosen/invalid file!" });
        }

        // Update username
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUsername(string newUsername)
        {
            // Find the currently-logged in user by current username
            var currentUsername = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == currentUsername).FirstOrDefault<User>()!;

            user.UserName = newUsername;
            user.NormalizedUserName = newUsername.ToUpper();

            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                context.SaveChanges();
                return Json("updated");
            }
            else
            {
                return Json("failed");
            }
        }

        // Update username
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateBio(string bio)
        {
            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            user.Bio = bio;
            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                context.SaveChanges();
                return Json("updated");
            }
            else
            {
                return Json("failed");
            }
        }

        // Update user's career phase
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateCareerPhase(string id)
        {
            int careerPhaseId = Convert.ToInt32(id);

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            // Update the logged-in user's careerPhaseId (foreign key)
            user.CareerPhaseId = careerPhaseId;

            // Update the logged-in user's careerPhase Navigation Property
            CareerPhase cp = context!.CareerPhases.Where(c => c.CareerPhaseId == careerPhaseId).FirstOrDefault<CareerPhase>()!;
            user.CareerPhase = cp;

            // Update the user properties
            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {   
                // Update the dtabase
                context.SaveChanges();
                return Json("updated");
            }
            else
            {
                return Json("failed");
            }
        }

        // Update user's experience level
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateExperienceLevel(string id)
        {
            int experienceLevelId = Convert.ToInt32(id);

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            // Update the logged-in user's careerPhaseId (foreign key)
            user.ExperienceLevelId = experienceLevelId;

            // Update the logged-in user's careerPhase Navigation Property
            ExperienceLevel el = context!.ExperienceLevels.Where(e => e.ExperienceLevelId == experienceLevelId).FirstOrDefault<ExperienceLevel>()!;
            user.ExperienceLevel = el;

            // Update the user properties
            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                // Update the dtabase
                context.SaveChanges();
                return Json("updated");
            }
            else
            {
                return Json("failed");
            }
        }


        // Update user's languages
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateLanguages(int[] ids)
        {
            
            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            List<NaturalLanguage> nlanguages = context.NaturalLanguages.ToList();

            user.NaturalLanguageUsers.Clear();

            foreach(NaturalLanguage n in nlanguages)
            {
                int id = n.NaturalLanguageId;
                if (ids.Contains(id))
                {
                    user.NaturalLanguageUsers!.Add(new NaturalLanguageUser
                    {
                        SlackId = user.SlackId!,
                        NaturalLanguageId = id,
                        NaturalLanguage = n,
                        User = user
                    });
                }
            }


            // Update the user properties
            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                // Update the dtabase
                context.SaveChanges();

                return Json("updated");
            }
            else
            {
                return Json("failed");
            }
        }

        // Update user's languages
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProgrammingLanguages(int[] ids)
        {

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            List<ProgrammingLanguage> planguages = context.ProgrammingLanguages.ToList();

            user.ProgrammingLanguageUsers.Clear();

            foreach (ProgrammingLanguage p in planguages)
            {
                int id = p.ProgrammingLanguageId;

                if (ids.Contains(id))
                {
                    user.ProgrammingLanguageUsers!.Add(new ProgrammingLanguageUser
                    {
                        SlackId = user.SlackId!,
                        ProgrammingLanguageId = id,
                        ProgrammingLanguage = p,
                        User = user
                    });
                }
            }


            // Update the user properties
            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                // Update the dtabase
                context.SaveChanges();

                return Json("updated");
            }
            else
            {
                return Json("failed");
            }
        }

        // Update user's Computer Science Interests
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateCSInterests(int[] ids)
        {

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            List<CSInterest> interests = context.CSInterests.ToList();

            user.CSInterestUsers.Clear();

            foreach (CSInterest i in interests)
            {
                int id = i.CSInterestId;

                if (ids.Contains(id))
                {
                    user.CSInterestUsers!.Add(new CSInterestUser
                    {
                        SlackId = user.SlackId!,
                        CSInterestId = id,
                        CSInterest = i,
                        User = user
                    });
                }
            }


            // Update the user properties
            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                // Update the dtabase
                context.SaveChanges();

                return Json("updated");
            }
            else
            {
                return Json("failed");
            }
        }

        // Update user's hobbies and interests
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateHobbies(int[] ids)
        {

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            List<Hobby> hobbies = context.Hobbies.ToList();

            user.HobbyUsers.Clear();

            foreach (Hobby h in hobbies)
            {
                int id = h.HobbyId;

                if (ids.Contains(id))
                {
                    user.HobbyUsers!.Add(new HobbyUser
                    {
                        SlackId = user.SlackId!,
                        HobbyId = id,
                        Hobby = h,
                        User = user
                    });
                }
            }


            // Update the user properties
            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                // Update the dtabase
                context.SaveChanges();

                return Json("updated");
            }
            else
            {
                return Json("failed");
            }
        }

        // Generate a random question that the user hasn't answered yet
        [Authorize]
        [HttpGet]
        public IActionResult GenerateRandomQuestion()
        {
            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            List<Question> questions = context.Questions.ToList();

            // User's QA-blocks consisting of questions and answers that user has typed in
            List<QuestionAnswerBlock> userQABlocks = user.QuestionAnswerBlocks.ToList();

            // Will store the strings of the questions that the user has already answered
            List<string> usersQuestionStrings = new List<string>();

            foreach (QuestionAnswerBlock qaBlock in userQABlocks)
            {
                usersQuestionStrings.Add(qaBlock.QuestionString);
            }

            // Stores questions user has not answered yet
            List<Question> notYetAnswered = new List<Question>();

            // Iterates through the questions in the database
            foreach (Question q in questions)
            {   
                // If the user's question-strings do not contain the current question
                // add the question to the returned list
                if (!usersQuestionStrings.Contains(q.QuestionString))
                {
                    notYetAnswered.Add(q);
                }
            }


            // Now pick a random question from those not yet answered
            var random = new Random();
            int index = random.Next(notYetAnswered.Count);

            // Check if there are any questions left unanswered first
            if (notYetAnswered.Count > 0)
            {
                Question randomQuestion = notYetAnswered[index];
                //string randomQuestionString = randomQuestion.QuestionString;

                return Json(new { output = JsonConvert.SerializeObject(randomQuestion) });
            }
            else
            {
                return Json(new { output = "" });
            }

        }


        // Update random question-answer block for the logged-in user
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateRandomQuestion(string id, string answer)
        {
            int questionId = Convert.ToInt32(id);
            Question q = context!.Questions.Where(q => q.QuestionId == questionId).FirstOrDefault<Question>()!;


            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            // Create a new Question-Answer block
            QuestionAnswerBlock qaBlock = new QuestionAnswerBlock
            {
                SlackId = user.SlackId,
                User = user,
                QuestionString = q.QuestionString,
                AnswerString = answer
            };

            // Add it to the database
            context.QuestionAnswerBlocks.Add(qaBlock);

            // Add it to the user
            user.QuestionAnswerBlocks!.Add(qaBlock);

            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                context.SaveChanges();
                return Json("updated");
            }
            else
            {
                return Json("failed");
            }

        }
        
        // Delete random question-answer block for the logged-in user
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteRandomQuestion(string id)
        {  
            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;


            int qaId = Convert.ToInt32(id);
            // Get the correct Question Answer Block
            QuestionAnswerBlock qaBlock = context!.QuestionAnswerBlocks.Where(q => q.QuestionAnswerBlockId == qaId).FirstOrDefault<QuestionAnswerBlock>()!;

            context.QuestionAnswerBlocks.Remove(qaBlock);

            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                context.SaveChanges();
                return Json("updated");
            }
            else
            {
                return Json("failed");
            }

        }



        // Delete random question-answer block for the logged-in user
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditRandomQuestion(string questionAnswerBlockId, string newAnswer)
        {
            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            int qaId = Convert.ToInt32(questionAnswerBlockId);

            // Get the correct Question Answer Block
            QuestionAnswerBlock qaBlock = context!.QuestionAnswerBlocks.Where(q => q.QuestionAnswerBlockId == qaId).FirstOrDefault<QuestionAnswerBlock>()!;

            qaBlock.AnswerString = newAnswer;

            var result = await userManager.UpdateAsync(user);

            // If the IdentityResult object is true, then sign the user in using a session cookie
            if (result.Succeeded)
            {
                context.SaveChanges();
                return Json("updated");
            }
            else
            {
                return Json("failed");
            }

        }

        // Check the user's password (through Ajax call in JS script)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CheckPassword(string password)
        {
            // Gets the currently-logged in user
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            var result = await userManager.CheckPasswordAsync(user, password);

            if (result)
            {

                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }


        //Change the user's password
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            // Gets the currently - logged in user
           var username = User.Identity!.Name;
           User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            //Result of the change password Identity operation
            IdentityResult result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (result.Succeeded)
            {
                context.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        // Deletes the user from the database
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            // Gets the currently - logged in user
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            user.DeactivateRequest = true;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                context.SaveChanges();
                await signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Json(false);
            }
        }

    } 
}

