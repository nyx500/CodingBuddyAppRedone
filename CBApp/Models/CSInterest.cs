namespace CBApp.Models
{   
    // Computer Science interests, e.g. AI, Web Development
    public class CSInterest
    {
        public int CSInterestId { get; set; }
        public string Name { get; set; }

        public ICollection<CSInterestUser> CSInterestUsers { get; set; }
    }
}
