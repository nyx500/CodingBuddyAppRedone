// Users whon liked the current user but they have not liked them back

namespace CBApp.Models
{
    public class UserWhoLikedTheUserViewModel
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public string SlackId { get; set; }

        // Picture data stored in format that can be displayed in Html
        // Use the static method in PotentialMatchUserViewModel to get this from the Byte array stored in the User class
        public string PictureString { get; set; }

    }
}
