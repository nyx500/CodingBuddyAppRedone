// Users whom the user likes and who like the user back

namespace CBApp.Models
{
    public class MatchedUserViewModel
    {
        public string UserName { get; set;}
        public string Id { get; set; }
        public string SlackId { get; set; }

        // Picture data stored in format that can be displayed in Html
        // Use the static method in PotentialMatchUserViewModel to get this from the Byte array stored in the User class
        public string PictureString { get; set; }


    }
}
