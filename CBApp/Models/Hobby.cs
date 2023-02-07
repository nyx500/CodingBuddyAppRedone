using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CBApp.Models
{
    public class Hobby
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Hobby")]
        public int HobbyId { get; set; }
        public string Name { get; set; }
        public ICollection<HobbyUser> HobbyUsers { get; set; }

    }
}
