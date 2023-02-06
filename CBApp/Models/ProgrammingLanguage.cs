namespace CBApp.Models
{
    public class ProgrammingLanguage
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
        public ICollection<ProgrammingLanguageUser> ProgrammingLanguageUsers { get; set; }
    }
}
