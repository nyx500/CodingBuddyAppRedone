using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Models
{
    public class Answer
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }
        public string AnswerString { get; set; }
    }
}
