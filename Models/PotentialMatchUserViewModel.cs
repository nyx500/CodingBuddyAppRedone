/** View Model for every potential-match user outputted by Find-a-Buddy process that is then
 stored in a list in PotentialMatchesViewModel
*/

namespace CBApp.Models
{
    public class PotentialMatchUserViewModel
    {
        // Default Identity User ID to be able to click on full profile
        public string Id { get; set; }

        // Username
        public string Username { get; set; }

        // Profile picture data properties
        public byte[]? Picture { get; set; }
        public string? PictureFormat { get; set; }
        public string? PictureStringForHtml { get; set; }


        // Selected data: career phase, experience level, Computer Science interests
        public string CareerPhaseName { get; set; }
        public string ExperienceLevelName { get; set; }
        public List<CSInterest> Interests { get; set; } = new List<CSInterest>();

        // Score of how strong the match is
        public int score { get; set; }

        // Returns an object of this type (but remember to fill in CSInterests in Ctrller!) from a user object
        public static PotentialMatchUserViewModel parseUser(User user)
        {
            PotentialMatchUserViewModel thisUser = new PotentialMatchUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Picture = user.Picture,
                PictureFormat = user.PictureFormat,
                CareerPhaseName = user.CareerPhase.Name,
                ExperienceLevelName = user.ExperienceLevel.Name
            };

            // Convert picture to string that can be displayed in HTML file
            if (thisUser.PictureFormat  != null) 
            {
                string picFormat = user.PictureFormat!.Substring(1);
                string mimeType = "image/" + picFormat;
                string base64 = Convert.ToBase64String(user.Picture);
                thisUser.PictureStringForHtml = string.Format("data:{0};base64,{1}", mimeType, base64);
            }
            else
            {
                thisUser.PictureStringForHtml = null;
            }

            return thisUser;
        }

    }
}
