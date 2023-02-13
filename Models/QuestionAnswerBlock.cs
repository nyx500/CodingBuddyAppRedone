using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CBApp.Models
{
    public class QuestionAnswerBlock
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Question and Answer")]
        public int QuestionAnswerBlockId;


        [Key]
        public string SlackId { get; set; } // foreign key
        public virtual User User { get; set; } // navigation property

        [Required]
        [StringLength(200)]
        public string QuestionString { get; set; }
        public string AnswerString { get; set; }

    }
}
