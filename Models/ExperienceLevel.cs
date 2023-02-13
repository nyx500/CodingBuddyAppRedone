using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CBApp.Models
{
    public class ExperienceLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Experience Level")]
        public int ExperienceLevelId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<User>? Users { get; set; } // navigation property for one-to-many relationship
    }
}
