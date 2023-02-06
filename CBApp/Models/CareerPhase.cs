namespace CBApp.Models
{
    public class CareerPhase
    {
        public int CareerPhaseId { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } // navigation property for one-to-many relationship

    }
}
