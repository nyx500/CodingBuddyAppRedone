// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class LikesConfig : IEntityTypeConfiguration<Likes>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<Likes> entity)
        {
            // Cleans up code in moderlBuilder in the ApplicationDbContext by storing it here instead
            // Sets up the properties included in the table for the User (IdentityUser) class
            entity.HasKey(l => new {l.SlackId1, l.SlackId2});

            entity.HasOne(x => x.User1)
                .WithMany( y => y.UsersLiked)
                .HasForeignKey(x => x.SlackId1)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.User2)
                .WithMany(y => y.LikedBy)
                .HasForeignKey(x => x.SlackId2);

            // Means --> User with SlackId "U73VQP71" likes the user with the Slack Id "8UJBW6F3"
            //entity.HasData(new Likes { SlackId1 = "U73VQP71", SlackId2 = "8UJBW6F3" });
            //// Otherway round
            //entity.HasData(new Likes { SlackId1 = "8UJBW6F3", SlackId2 = "U73VQP71" });

        }
    }
}
