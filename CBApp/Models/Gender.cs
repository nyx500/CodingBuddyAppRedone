using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CBApp.Models
{
    public class Gender
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } // navigation property for one-to-many relationship
    }
}
