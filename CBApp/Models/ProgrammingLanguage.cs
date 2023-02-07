using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CBApp.Models
{
    public class ProgrammingLanguage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name="Programming Language")]
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
        public ICollection<ProgrammingLanguageUser> ProgrammingLanguageUsers { get; set; }
    }
}
