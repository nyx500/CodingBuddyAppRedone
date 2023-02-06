/** This is the class which will be the basis of the Join Table in the database to configure the many
 -to-many relationship between a User and their hobbies and interests */

using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Models
{
    public class HobbyUser
    {
        // Composite primary key

        [Column("SlackId")]
        public string SlackId { get; set; } // Foreign key for the user: a unique Slack ID
        public int HobbyId { get; set; } // Foreign key for natural language

        // Navigation properties
        public User User { get; set; }
        public Hobby Hobby { get; set; }
    }
}
