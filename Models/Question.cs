using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Please enter a question string!")]
        [StringLength(200, ErrorMessage = "String length is 200!")]
        public string QuestionString { get; set; }

    }
}
