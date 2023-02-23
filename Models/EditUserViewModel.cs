using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CBApp.Models
{
    public class EditUserViewModel
    {
        public string? ImageSrc { get; set; }
        public string? UserName { get; set;}
        public string? SlackId { get; set; }

        public string? Bio { get; set; }

        public CareerPhase? CareerPhase { get; set; }
        public ExperienceLevel? ExperienceLevel { get; set; }
        public List<String>? LanguageNames { get; set; }



    }
}
