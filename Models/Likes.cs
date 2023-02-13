using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Models
{
    public class Likes
    {
        [Column("Liker User")]
        public string SlackId1 { get; set; }
        public virtual User User1 { get; set; }


        [Column("Liked User")]
        public string SlackId2 { get; set; }

        public virtual User User2 { get; set; }

        // Set to true if the first user's like is returned
        [Column("IsMatch")]
        public bool IsMatch { get; set; }

    }
}
