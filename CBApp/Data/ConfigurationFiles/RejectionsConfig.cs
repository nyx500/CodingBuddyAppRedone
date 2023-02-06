// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class RejectionsConfig : IEntityTypeConfiguration<Rejections>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<Rejections> entity)
        {
            // Cleans up code in moderlBuilder in the ApplicationDbContext by storing it here instead
            // Sets up the properties included in the table for the User (IdentityUser) class
            entity.HasKey(r => new { r.SlackId1, r.SlackId2 });

            entity.HasOne(x => x.User1)
                .WithMany(y => y.UsersRejected)
                .HasForeignKey(x => x.SlackId1)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.User2)
                .WithMany(y => y.RejectedBy)
                .HasForeignKey(x => x.SlackId2);

        }
    }
}