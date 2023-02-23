using CBApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CBApp.Models
{

    public class User : IdentityUser
    {
        // Inherits IdentityUser properties


        // Required fields
        [Required(ErrorMessage = "Please enter a Slack ID!")]
        [StringLength(50)]
        [Key]
        [Display(Name ="*Slack ID")]
        public string? SlackId { get; set; }

        [Required(ErrorMessage = "Please enter a Username!")]
        [StringLength(70)]
        [Display(Name = "*Username")]
        public override string?  UserName  { get; set; }


        // Many-to-one relationships
        [Required(ErrorMessage = "Please enter your career phase!")]
        public int CareerPhaseId { get; set; } // foreign key
        [Display(Name = "Career Phase")]
        public virtual CareerPhase? CareerPhase { get; set; } // navigation property


        [Required(ErrorMessage = "Please enter your experience level!")]
        public int ExperienceLevelId { get; set; } // foreign key
        public virtual ExperienceLevel? ExperienceLevel { get; set; } // navigation property

        // Many-to-many relationships

        // ICollection variables storing lists of natural and programming languages
        public virtual ICollection<NaturalLanguageUser>? NaturalLanguageUsers { get; set; }

        //[Required(ErrorMessage = "Please enable selection of favourite programming languages!")]
        public virtual ICollection<ProgrammingLanguageUser>? ProgrammingLanguageUsers { get; set; }

        //[Required(ErrorMessage = "Please enable selection of Computer Science interests!")]
        public virtual ICollection<CSInterestUser>? CSInterestUsers { get; set; }

        //[Required(ErrorMessage = "Please select some hobbies and interests!")]
        public virtual ICollection<HobbyUser>? HobbyUsers { get; set; }


        // Users' relationships with other users (many-to-many rels)
        public virtual ICollection<Likes>? UsersLiked { get; set; }
        public virtual ICollection<Likes>? LikedBy { get; set; }

        public virtual ICollection<Rejections>? UsersRejected { get; set; }
        public virtual ICollection<Rejections>? RejectedBy { get; set; }

        // Users' questions & answers for "Random Question Generator"
        public virtual ICollection<QuestionAnswerBlock>? QuestionAnswerBlocks { get; set; }


        // Optional fields
        [StringLength(500)]
        public string? Bio { get; set; }
        public int? GenderId { get; set; } // foreign key
        public virtual Gender? Gender { get; set; } // navigation property

        // Attribution: https://www.johansmarius.dev/2021/12/using-pictures-in-aspnet-mvc-core-with.html
        public byte[]? Picture { get; set; }
        public string? PictureFormat { get; set; }


    }
}