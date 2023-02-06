namespace CBApp.Models
{
    public class NaturalLanguage
    {
        public int NaturalLanguageId { get; set; }
        public string Name { get; set; }

        public ICollection<NaturalLanguageUser> NaturalLanguageUsers { get; set; }
    }

}
