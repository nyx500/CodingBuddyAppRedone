using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Models
{
    public class Rejections
    {
        [Column("Rejecting User")]
        public string SlackId1 { get; set; }
        public virtual User User1 { get; set; }


        [Column("Rejected User")]
        public string SlackId2 { get; set; }

        public virtual User User2 { get; set; }

    }
}
