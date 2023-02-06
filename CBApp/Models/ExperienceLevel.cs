namespace CBApp.Models
{
    public class ExperienceLevel
    {
        public int ExperienceLevelId { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } // navigation property for one-to-many relationship
    }
}
