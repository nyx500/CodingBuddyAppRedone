namespace CBApp.Models
{
    public class MatchesPageViewModel
    {

        // Consists of three different lists of users for the Matches page: matches (mutually liked), users whom the user liked who have not liked the user back,
        // and likers (users who have liked the user but whose like the user has not returned)
        public List<MatchedUserViewModel> matchedUsers = new List<MatchedUserViewModel>();
        public List<LikedButNotMatchedUserViewModel> likedButNotMatchedUsers = new List<LikedButNotMatchedUserViewModel>();
        public List<UserWhoLikedTheUserViewModel> likers = new List<UserWhoLikedTheUserViewModel>();

    }
}
