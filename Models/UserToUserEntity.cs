/** This is the class which will be the basis of the Join Table in the database to configure the many
 -to-many relationship between a User and a Programming Language they know */

using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Models
{
    public class UserToUserEntity
    {
        // Composite primary key
        public string SlackId1 { get; set; }
        public virtual User User1 { get; set; }

        public string SlackId2 { get; set; }
        public virtual User User2 { get; set; }

    }
}
