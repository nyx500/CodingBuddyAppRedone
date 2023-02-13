/** This is the class which will be the basis of the Join Table in the database to configure the many
 -to-many relationship between a User and a Programming Language they know */

using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Models
{
    public class ProgrammingLanguageUser
    {
        // Composite primary key

        [Column("SlackId")]
        public string SlackId { get; set; } // Foreign key for the user: a unique Slack ID
        public int ProgrammingLanguageId { get; set; } // Foreign key for natural language

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
