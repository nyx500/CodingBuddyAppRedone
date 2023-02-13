// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class ProgrammingLanguageUserConfig : IEntityTypeConfiguration<ProgrammingLanguageUser>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<ProgrammingLanguageUser> entity)
        {
              entity.HasKey(pu => new { pu.SlackId, pu.ProgrammingLanguageId });
           
              entity.HasOne(pu => pu.User)
                .WithMany(u => u.ProgrammingLanguageUsers)
                .HasForeignKey(pu => pu.SlackId);

              entity.HasOne(pu => pu.ProgrammingLanguage)
                .WithMany(p => p.ProgrammingLanguageUsers)
                .HasForeignKey(pu => pu.ProgrammingLanguageId);

            // Sets up the test many-to-many relationship bectween the test user and their favourite programming languages
            //entity.HasData(
            //        new ProgrammingLanguageUser { SlackId = "U73VQP71", ProgrammingLanguageId = 3 }
            //         );
            //   entity.HasData(
            //        new ProgrammingLanguageUser { SlackId = "U73VQP71", ProgrammingLanguageId = 5 }
            //        );
            //   entity.HasData(
            //        new ProgrammingLanguageUser { SlackId = "U73VQP71", ProgrammingLanguageId = 6 }
            //        );
        }
    }
}
