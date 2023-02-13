// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class AnswerConfig : IEntityTypeConfiguration<Answer>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<Answer> entity)
        {
            entity.Property(n => n.AnswerId);
            entity.Property(n => n.AnswerString);

        }
    }
}
