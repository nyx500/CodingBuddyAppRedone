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

            // Find the currently-logged in user by current username
            var currentUsername = User.Identity!.Name;
            User currentUser = context!.Users.Where(u => u.UserName == currentUsername).FirstOrDefault<User>()!;

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
            List<User> users = new List<User>();
            // Use a bool to keep track of which users to add because apparently List.Remove() throws a massive error
            bool shouldIAddTheUser;

            // Only add users that have not already been passed on / rejected or already liked
            foreach(User user in context.Users.ToList())
            {
                shouldIAddTheUser = true;
                foreach (Rejections rej in context.Rejections.ToList())
                {
                    // If current user has already passed this user / this user has passed the current user
                    if (((rej.SlackId1 == currentUser.SlackId && rej.SlackId2 == user.SlackId)
                        || (rej.SlackId1 == user.SlackId && rej.SlackId2 == currentUser.SlackId)))
                    {
                        shouldIAddTheUser = false;
                    }
                }

                // Check if user has been liked already
                foreach (Likes like in context.Likes.ToList())
                {
                    if ((like.SlackId1 == currentUser.SlackId && like.SlackId2 == user.SlackId))
                    {
                        shouldIAddTheUser = false;
                    }
                }

                // Finally check if the user is the currently logged-in user and don't add them if they are
                if ((currentUser == user))
                {
                    shouldIAddTheUser = false;
                }

                if (shouldIAddTheUser)
                {   
                    users.Add(user);
                }

            }

            // If all the inputs were empty, just return all the users
            if (SelectedCareerPhaseIds.Count == 0 && SelectedExperienceLevelIds.Count == 0
                && SelectedProgrammingLanguageIds.Count == 0 && SelectedCSInterestsIds.Count == 0
                && SelectedHobbyIds.Count == 0)
            {
                // Add all users to the model
                foreach (User user in users)
                {
                    // Turn user into a PotentialMatchUserViewModel to add them to the model
                    PotentialMatchUserViewModel potentialMatch = PotentialMatchUserViewModel.parseUser(user);
                    // Add CS interests to the potentialMatch
                    foreach (CSInterestUser csiu in user.CSInterestUsers)
                    {
                        potentialMatch.Interests.Add(csiu.CSInterest);
                    }

                    // Add 1 to the potential user's match-score (all will have one if no options have been selected)
                    potentialMatch.score = 1;
                    unsortedModel.matches.Add(potentialMatch);

                }
                return View("PotentialMatches", unsortedModel);
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


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LikeUser(string id)
        {

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            // Find the liked user
            User likedUser = context.Users.Where(u => u.Id == id).FirstOrDefault<User>();

            // Check if like-relationship already exists in the database
            if (context.Likes.Any(l => l.SlackId1 == user.SlackId && l.SlackId2 == likedUser.SlackId))
            {   
                // If it exists, we don't have to do anything --> exit the method
                return Json(true);
            }


            List<Likes> likes = context.Likes.ToList();


            // Generate the new like-relationship
            Likes like = new Likes { SlackId1 = user.SlackId, User1 = user, SlackId2 = likedUser.SlackId, User2 = likedUser };

            // Add likes to database
            likes.Add(like);
            user.UsersLiked.Add(like);
            likedUser.LikedBy.Add(like);


            // Update the user properties
            var result = await userManager.UpdateAsync(user);
            
            // Check if the update succeeded
            if (result.Succeeded)
            {
                // Update the user properties
                var result2 = await userManager.UpdateAsync(likedUser);
                if (result2.Succeeded)
                {
                    context.SaveChanges();

                    // Checks if the like is mutual and sets the isMatched property of the likes to "true" if this is the case
                    checkForAndSetMatch(user, likedUser, context);

                    return Json(true);
                }
                else
                { 
                    return Json(false); 
                }
            }

            return Json(false);
        }


        // A helper method for the above "LikeUser" method in this controller than determines whether two users have mutually liked one another and if so sets the isMatched property
        // in the context "Likes" table to "true"
        public void checkForAndSetMatch(User currentUser, User targetUser, ApplicationDbContext context)
        {
            bool doesCurrentUserLikeTargetUser = context.Likes.Any(l => l.SlackId1 == currentUser.SlackId && l.SlackId2 == targetUser.SlackId);
            bool doesTargetUserLikeCurrentUser = context.Likes.Any(l => l.SlackId1 == targetUser.SlackId && l.SlackId2 == currentUser.SlackId);

            // If the users have liked each other, set the isMatched property to true
            if (doesCurrentUserLikeTargetUser && doesTargetUserLikeCurrentUser)
            {
                Likes likeRelationship1 = context.Likes.Where(l => l.SlackId1 == currentUser.SlackId && l.SlackId2 == targetUser.SlackId).FirstOrDefault<Likes>();

                if (!likeRelationship1.IsMatch)
                {
                    likeRelationship1.IsMatch = true;
                }

                Likes likeRelationship2 = context.Likes.Where(l => l.SlackId1 == targetUser.SlackId && l.SlackId2 == currentUser.SlackId).FirstOrDefault<Likes>();

                if (!likeRelationship2.IsMatch)
                {
                    likeRelationship2.IsMatch = true;
                }

                context.SaveChanges();
            }
            // The likes are not mutual, because the current user likes the target user but the "like" is not returned: find the relationship and set its "isMatched" property to false
            else if (doesCurrentUserLikeTargetUser && !doesTargetUserLikeCurrentUser)
            {
                Likes likeRelationship = context.Likes.Where(l => l.SlackId1 == currentUser.SlackId && l.SlackId2 == targetUser.SlackId).FirstOrDefault<Likes>();

                if (!likeRelationship.IsMatch)
                {
                    likeRelationship.IsMatch = false;
                }

            }
            // The likes are not mutual, because the target user likes the current user but the "like" is not returned: find the relationship and set its "isMatched" property to false
            else if (!doesCurrentUserLikeTargetUser && doesTargetUserLikeCurrentUser)
            {
                Likes likeRelationship = context.Likes.Where(l => l.SlackId1 == targetUser.SlackId && l.SlackId2 == currentUser.SlackId).FirstOrDefault<Likes>();

                if (!likeRelationship.IsMatch)
                {
                    likeRelationship.IsMatch = false;
                }
            }
        }


        [Authorize]
        [HttpPost]
        /** If a user has "liked" a user, then "unlike" them */
        public async Task <IActionResult> UnlikeUser(string id)
        {
            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User currentUser = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            // Find the unliked user
            User unlikedUser = context.Users.Where(u => u.Id == id).FirstOrDefault<User>();

            // Find the like-relationship to remove
            Likes targetedLike = context.Likes.Where(like => like.SlackId1 == currentUser.SlackId).Where(like => like.SlackId2 == unlikedUser.SlackId).FirstOrDefault<Likes>()!;

            context.Likes.Remove(targetedLike);
            context.SaveChanges();

            // Update the user properties
            var result = await userManager.UpdateAsync(currentUser);

            // Check if the update succeeded
            if (result.Succeeded)
            {
                // Update the user properties
                var result2 = await userManager.UpdateAsync(unlikedUser);
                if (result2.Succeeded)
                {
                    context.SaveChanges();

                    // Changes isMatched field for the users' relationship to "false" if it was true before the "unlike" event happened
                    checkForAndSetMatch(currentUser, unlikedUser, context);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            return Json(false);
        }


        [Authorize]
        [HttpPost]
        /** Pass/Rejects a user */
        public async Task<IActionResult> PassUser(string id)
        {

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            // Find the passed user
            User passedUser = context.Users.Where(u => u.Id == id).FirstOrDefault<User>();

            List<Rejections> rejections = context.Rejections.ToList();

            Rejections rejection = new Rejections { SlackId1 = user.SlackId, User1 = user, SlackId2 = passedUser.SlackId, User2 = passedUser };

            // Add rejections to database
            rejections.Add(rejection);
            user.UsersRejected.Add(rejection);
            passedUser.RejectedBy.Add(rejection);


            // Update the user properties
            var result = await userManager.UpdateAsync(user);

            // Check if the update succeeded
            if (result.Succeeded)
            {
                // Update the user properties
                var result2 = await userManager.UpdateAsync(passedUser);
                if (result2.Succeeded)
                {
                    context.SaveChanges();

                    // If one of the users liked the other and there has been a rejection from either side, set "isMatched" in the Likes table to false
                    checkForAndSetMatch(user, passedUser, context);

                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }

            return Json(false);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ResetRejections()
        {
            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User user = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            user.UsersRejected.Clear();
            // Update the user properties
            var result = await userManager.UpdateAsync(user);

            // Check if the update succeeded
            if (result.Succeeded)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewProfile(string id)
        {

            ProfileViewModel model = new ProfileViewModel();

            // Find the currently-logged in user by username
            var username = User.Identity!.Name;
            User currentUser = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            User user = context.Users.Where(u => u.Id == id).FirstOrDefault();


            if (user != null)
            {   
                // Set the long unique Id for the user (to do things in AJAX with hidden inputs)
                model.Id = user.Id;
                // Set the username for the user view model
                model.UserName = user.UserName;
                // Set the biography for the user view model
                model.Bio = user.Bio;

                // Set picture data if there is some in the database for that user
                if (user.PictureFormat != null)
                {
                    model.ImageSrc = PotentialMatchUserViewModel.BytesToString_Picture(user.Picture, user.PictureFormat);
                }
                model.CareerPhase = user.CareerPhase;
                model.ExperienceLevel = user.ExperienceLevel;
                
                // Only display the user's slackID if they have liked the currently logged-in user
                if ((context.Likes.ToList().FindIndex(f => f.SlackId1 == user.SlackId) != -1) &&
                        (context.Likes.ToList().FindIndex(f => f.SlackId2 == currentUser.SlackId) != -1))
                {
                    model.SlackId = user.SlackId;
                }
                else
                {
                    model.SlackId = "";
                }
                
                // Default: set the field that determines if the logged-in user likes the viewed user to "false", so that it's never "null"
                model.hasBeenLiked = false;

                // Determines if the current(logged-in) user has liked this user and sets the ViewModel "hasBeenLiked" field
                foreach (Likes l in context.Likes.ToList())
                {   
                    // Check if there exists a "likes" many-to-many entry where the currentUser has "liked" the viewed user...
                    if (l.SlackId1 == currentUser.SlackId && l.SlackId2 == user.SlackId)
                    {
                        // If the current user has liked the view user, set the hasBeenLiked property to true
                        model.hasBeenLiked = true;
                    }
                }

                // Default: set the field that determines if the logged-in user has rejected the viewed user to "false", so that it's never "null"
                model.hasBeenRejected = false;
                // Determines if the current(logged-in) user has rejected this user and sets the ViewModel "hasBeenRejected" field
                foreach (Rejections r in context.Rejections.ToList())
                {
                    // Check if there exists a "rejections" many-to-many entry where the currentUser has "rejected" the viewed user...
                    if (r.SlackId1 == currentUser.SlackId && r.SlackId2 == user.SlackId)
                    {
                        // If the current user has rejected the view user, set the hasBeenLiked property to true
                        model.hasBeenLiked = true;
                    }
                }


                // Get names of preferences
                foreach (NaturalLanguageUser languser in user.NaturalLanguageUsers)
                {
                    model.LanguageNames.Add(languser.NaturalLanguage.Name);
                }
                foreach (ProgrammingLanguageUser languser in user.ProgrammingLanguageUsers)
                {
                    model.ProgrammingLanguageNames.Add(languser.ProgrammingLanguage.Name);
                }
                foreach (CSInterestUser c in user.CSInterestUsers)
                {
                    model.CSInterestNames.Add(c.CSInterest.Name);
                }
                foreach (HobbyUser h in user.HobbyUsers)
                {
                    model.HobbyNames.Add(h.Hobby.Name);
                }
                // Get QuestionAnswer blocks
                foreach(QuestionAnswerBlock qb in user.QuestionAnswerBlocks)
                {
                    model.QuestionAnswerBlocks.Add(qb);
                }

                return View("ViewProfile", model);
            }
            else
            {
                return Content("No such user!");
            }

        }

        [HttpGet]
        [Authorize]
        public IActionResult Matches()
        {
            MatchesPageViewModel model = new MatchesPageViewModel();

            // Get the logged-in user
            var username = User.Identity!.Name;
            User currentUser = context!.Users.Where(u => u.UserName == username).FirstOrDefault<User>()!;

            // Get the list of users the logged in user has matched withn or liked and put them in the model lists
            foreach (Likes like in context.Likes.ToList())
            {   
                // If isMatch is true for a like-relationship the current use is involved with, add the second user to the matchedUsers list
                if (like.SlackId1 == currentUser.SlackId && like.IsMatch)
                {
                    MatchedUserViewModel matchedUser = new MatchedUserViewModel
                    {
                        UserName = like.User2.UserName,
                        Id = like.User2.Id,
                        SlackId = like.User2.SlackId,
                    };

                    // Add picture field only if the user has a profile picture
                    if (like.User2.PictureFormat != null && like.User2.Picture != null)
                    {
                        matchedUser.PictureString = PotentialMatchUserViewModel.BytesToString_Picture(like.User2.Picture, like.User2.PictureFormat);
                    }

                    model.matchedUsers.Add(matchedUser);
                }
                // User has liked this user but they haven't liked them back
                else if (like.SlackId1 == currentUser.SlackId && !like.IsMatch)
                {
                    LikedButNotMatchedUserViewModel likedUser = new LikedButNotMatchedUserViewModel
                    {
                        UserName = like.User2.UserName,
                        Id = like.User2.Id
                    };

                    if (like.User2.PictureFormat != null && like.User2.Picture != null)
                    {
                        likedUser.PictureString = PotentialMatchUserViewModel.BytesToString_Picture(like.User2.Picture, like.User2.PictureFormat);
                    }

                    model.likedButNotMatchedUsers.Add(likedUser);
                }
                // The other user has liked the current user but the current user has not liked them back
                else if (like.SlackId2 == currentUser.SlackId && !like.IsMatch)
                {
                    UserWhoLikedTheUserViewModel likerUser = new UserWhoLikedTheUserViewModel
                    {
                        UserName = like.User1.UserName,
                        Id = like.User1.Id,
                        SlackId = like.User1.SlackId,
                    };

                    if (like.User1.PictureFormat != null && like.User1.Picture != null)
                    {
                        likerUser.PictureString = PotentialMatchUserViewModel.BytesToString_Picture(like.User1.Picture, like.User1.PictureFormat);
                    }

                    model.likers.Add(likerUser);
                }
            }

            return View(model);
        }

    }
}
