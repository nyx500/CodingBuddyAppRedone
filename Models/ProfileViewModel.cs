// View model for looking at someone's profile

namespace CBApp.Models
{
    public class ProfileViewModel
    {   
            public string? ImageSrc { get; set; }
            public string? UserName { get; set; }
            public string? SlackId { get; set; }
            public string? Bio { get; set; }
            public CareerPhase? CareerPhase { get; set; }
            public ExperienceLevel? ExperienceLevel { get; set; }

            public List<String>? LanguageNames = new List<String>();
            public List<String>? ProgrammingLanguageNames = new List<String>();
            public List<String>? CSInterestNames = new List<String>();
            public List<String>? HobbyNames = new List<String>();
            public List<QuestionAnswerBlock>? QuestionAnswerBlocks = new List<QuestionAnswerBlock>();
    }

}