// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class ExperienceLevelConfig : IEntityTypeConfiguration<ExperienceLevel>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<ExperienceLevel> entity)
        {
            entity.Property(n => n.ExperienceLevelId);
            entity.Property(n => n.Name);

            foreach (int i in Enum.GetValues(typeof(EnumsForUser.ExperienceLevel)))
            {
                // Casts integer into the corresponding enum
                var enumLanguage = (EnumsForUser.ExperienceLevel)i;

                // Get the ExperienceLevel name from the enum as a string
                string enumAsString = enumLanguage.ToString();

                // Seed the DB with names and Ids of different ExperienceLevels
                entity.HasData(
                    new ExperienceLevel
                    {
                        // Initializes the first Id value with '1' instead of '0'
                        ExperienceLevelId = (i + 1),
                        Name = enumAsString
                    }
                );
            }
        }
    }
}
