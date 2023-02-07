using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CBApp.Models
{
    public class NaturalLanguage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Language")]
        public int NaturalLanguageId { get; set; }
        public string Name { get; set; }

        public ICollection<NaturalLanguageUser> NaturalLanguageUsers { get; set; }
    }

}
