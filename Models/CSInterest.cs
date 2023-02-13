using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CBApp.Models
{   
    // Computer Science interests, e.g. AI, Web Development
    public class CSInterest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Computer Science Interest ")]
        public int CSInterestId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CSInterestUser> CSInterestUsers { get; set; }
    }
}
