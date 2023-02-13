/** This is the class which will be the basis of the Join Table in the database to configure the many
 -to-many relationship between a User and a Programming Language they know */

using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Models
{
    public class UserToUserLikes
    {
        // Composite primary key

        [Column("SlackId")]
        public string SlackId { get; set; } // Foreign key for User1: a unique Slack ID

        // Navigation properties
        public virtual User User { get; set; }
    }
}
