using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using CBApp.Data.ConfigurationFiles;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Hosting.Server;


namespace CBApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder
            .UseLazyLoadingProxies().
                UseSqlServer("Server = tcp:codingbuddyappserver.database.windows.net, 1433; Initial Catalog = cbappdb; Persist Security Info = False; User ID = ophelia; Password =Gniezno55; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; ");
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<CareerPhase> CareerPhases { get; set; }
        public DbSet<ExperienceLevel> ExperienceLevels { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<NaturalLanguage> NaturalLanguages { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<CSInterest> CSInterests { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<NaturalLanguageUser> NaturalLanguageUsers { get; set; }
        public DbSet<ProgrammingLanguageUser> ProgrammingLanguageUsers { get; set; }
        public DbSet<CSInterestUser> CSInterestUsers { get; set; }
        public DbSet<HobbyUser> HobbyUsers { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Rejections> Rejections { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswerBlock> QuestionAnswerBlocks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Sets up the properties included in the table for the User (IdentityUser) class
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new GenderConfig());
            modelBuilder.ApplyConfiguration(new CareerPhaseConfig());
            modelBuilder.ApplyConfiguration(new ExperienceLevelConfig());
            modelBuilder.ApplyConfiguration(new NaturalLanguageConfig());
            modelBuilder.ApplyConfiguration(new ProgrammingLanguageConfig());
            modelBuilder.ApplyConfiguration(new CSInterestConfig());
            modelBuilder.ApplyConfiguration(new HobbyConfig());
            modelBuilder.ApplyConfiguration(new QuestionConfig());



            // Sets up the many-to-many relationship between natural/programming languages and Users
            modelBuilder.ApplyConfiguration(new NaturalLanguageUserConfig());
            modelBuilder.ApplyConfiguration(new ProgrammingLanguageUserConfig());
            modelBuilder.ApplyConfiguration(new CSInterestUserConfig());
            modelBuilder.ApplyConfiguration(new HobbyUserConfig());
            modelBuilder.ApplyConfiguration(new LikesConfig());
            modelBuilder.ApplyConfiguration(new RejectionsConfig());
            modelBuilder.ApplyConfiguration(new QuestionAnswerBlockConfig());

        }
    }
}
