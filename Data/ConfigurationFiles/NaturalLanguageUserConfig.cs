// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class NaturalLanguageUserConfig : IEntityTypeConfiguration<NaturalLanguageUser>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<NaturalLanguageUser> entity)
        {
            // Sets up the many-to-many relationship between Users and natural languages spoken
                entity.HasKey(nu => new { nu.SlackId, nu.NaturalLanguageId });

                entity.HasOne(nu => nu.User)
                .WithMany(u => u.NaturalLanguageUsers)
                .HasForeignKey(nu => nu.SlackId);

                entity.HasOne(nu => nu.NaturalLanguage)
                .WithMany(n => n.NaturalLanguageUsers)
                .HasForeignKey(nu => nu.NaturalLanguageId);


            // Sets up the test many-to-many relationship bectween the test user and their spoken languages
            //entity.HasData(
            //        new NaturalLanguageUser { SlackId = "U73VQP71", NaturalLanguageId = 89 }
            //    );
            //entity.HasData(
            //        new NaturalLanguageUser { SlackId = "U73VQP71", NaturalLanguageId = 28 }
            //    );
            //entity.HasData(
            //        new NaturalLanguageUser { SlackId = "U73VQP71", NaturalLanguageId = 45 }
            //    );
        }
    }
}
