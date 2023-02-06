namespace CBApp.Models
{
    public class Hobby
    {
        public int HobbyId { get; set; }
        public string Name { get; set; }
        public ICollection<HobbyUser> HobbyUsers { get; set; }

    }
}
