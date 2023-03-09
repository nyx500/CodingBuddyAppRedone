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
using Microsoft.IdentityModel.Tokens;

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

        
        /** Creates a RegisterViewModel from the DBContext and outputs the Registration Wizard view */
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

            // Create a new view model
            RegisterViewModel model = new RegisterViewModel();

            // Populates the view model with data from the database
            model.createModelFromDatabase(context);

            // Set state of whether the user had to redirect (due to input error) to this route to 0 (=false)
            HttpContext.Session.SetInt32("RedirectToRegistrationForm", 0);

            return View(model);
        }


        /** Gets the data the user submitted when registered, and if valid, sign the user in, otherwise display errors in model */
        [HttpPost]
        public async Task<IActionResult> RegisterInSteps(RegisterViewModel model)
        {   
            // Generate a list of errors if model is invaid
            List<string> errors = model.validateModelData();

            // If no errors are found, create the user
            if (errors.IsNullOrEmpty())
            {
                User user = model.createUser(context!);

                // Adds the new user with their data to the database
                context!.Users.Add(user);

                // Add user and hash the submitted password using the UserManager instance
                var result = await userManager.CreateAsync(user, model.Password);

                // If the IdentityResult object is true, then sign the user in
                if (result.Succeeded)
                {
                    context.SaveChanges();

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.Errors = new List<string>();
                    model.Errors.Add(result.Errors.ToString());

                    // Returns to the Registration page if the model is invalid - displays the errors in the form
                    return View(model);
                }
            }
            // Invalid data: return the items error list in the model and go to Register page again, displaying the errors
            else
            {
                foreach (string error in errors)
                {
                    model.Errors.Add(error);
                }
                return View(model);
            }

        }
        
        /** Logs the currently-signed in user out */
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

                if (user == null)
                {
                    ModelState.AddModelError("invalidLogin", "Sorry, this username does not exist.");
                    return View(model);
                }

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
                        // Redirect to the index page
                        return RedirectToAction("FindABuddy", "Matches");
                    }
                    else
                    {
                        ModelState.AddModelError("invalidLogin", "Invalid username/password combination.");
                        return View(model);
                    }
                }
            }

            ModelState.AddModelError("invalidLogin", "Invalid username/password combination.");
            return View(model);
        }



        /** Checks if the inputted username exists, returns JSON 'true' if already exists in database */
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



        /** Checks if the inputted SlackId exists, returns JSON 'true' if already exists in database */
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


        /** Authorize: Make this route only accessible if user is logged in! */
        /** Goes to the user's own profile page so they can edit it and populate the model with dropdowns/checkboxes for options */
        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            // Get the current user
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            EditUserViewModel model = new EditUserViewModel();
            model.createModelFromUser(user);
            model.createOptionsLists(context);

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

        // Update user's gender
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateGender(string id)
        {
            int genderId = Convert.ToInt32(id);

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            // Update the logged-in user's gender (foreign key)
            user.GenderId = genderId;

            if (genderId != 0)
            {
                // Update the logged-in user's gender Navigation Property
                Gender g = context!.Genders.Where(g => g.GenderId == genderId).FirstOrDefault<Gender>()!;
                user.Gender = g;
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

