// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class CSInterestUserConfig : IEntityTypeConfiguration<CSInterestUser>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<CSInterestUser> entity)
        {
            // Sets up the many-to-many relationship between Users and natural languages spoken
            entity.HasKey(cu => new { cu.SlackId, cu.CSInterestId });

            entity.HasOne(cu => cu.User)
            .WithMany(u => u.CSInterestUsers)
            .HasForeignKey(cu => cu.SlackId);

            entity.HasOne(nu => nu.CSInterest)
            .WithMany(n => n.CSInterestUsers)
            .HasForeignKey(nu => nu.CSInterestId);


            // Sets up the test many-to-many relationship bectween the test user and their spoken languages
            //entity.HasData(
            //        new CSInterestUser { SlackId = "U73VQP71", CSInterestId = 1 }
            //    );
            //entity.HasData(
            //        new CSInterestUser { SlackId = "U73VQP71", CSInterestId = 2 }
            //    );
            //entity.HasData(
            //        new CSInterestUser { SlackId = "U73VQP71", CSInterestId = 3 }
            //    );
        }
    }
}
