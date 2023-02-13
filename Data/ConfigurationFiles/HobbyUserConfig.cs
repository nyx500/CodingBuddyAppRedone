// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class HobbyUserConfig: IEntityTypeConfiguration<HobbyUser>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<HobbyUser> entity)
        {
            // Sets up the many-to-many relationship between Users and selected hobbies
            entity.HasKey(nu => new { nu.SlackId, nu.HobbyId });

            entity.HasOne(nu => nu.User)
            .WithMany(u => u.HobbyUsers)
            .HasForeignKey(nu => nu.SlackId);

            entity.HasOne(nu => nu.Hobby)
            .WithMany(n => n.HobbyUsers)
            .HasForeignKey(nu => nu.HobbyId);


            // Sets up the test many-to-many relationship bectween the test user and their spoken languages
            //entity.HasData(
            //        new HobbyUser { SlackId = "U73VQP71", HobbyId = 10 }
            //    );
            //entity.HasData(
            //        new HobbyUser { SlackId = "U73VQP71", HobbyId = 21 }
            //    );
            //entity.HasData(
            //        new HobbyUser { SlackId = "U73VQP71", HobbyId = 43 }
            //    );
        }
    }
}
