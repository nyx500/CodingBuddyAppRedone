// Users who the current user liked but who have not liked the current user back

namespace CBApp.Models
{
    public class LikedButNotMatchedUserViewModel
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        // No SlackId property as they did not like the user back

        // Picture data stored in format that can be displayed in Html
        // Use the static method in PotentialMatchUserViewModel to get this from the Byte array stored in the User class
        public string PictureString { get; set; }

    }
}
