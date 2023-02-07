// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class ProgrammingLanguageConfig : IEntityTypeConfiguration<ProgrammingLanguage>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<ProgrammingLanguage> entity)
        {
            entity.Property(p => p.ProgrammingLanguageId);
            entity.Property(p => p.Name);

            foreach (int i in Enum.GetValues(typeof(EnumsForUser.ProgrammingLanguageName)))
            {
                // Casts integer into the corresponding enum
                var enumLanguage = (EnumsForUser.ProgrammingLanguageName)i;

                string enumAsString = enumLanguage.ToString();

                // Seed the DB with names and Ids of natural spoken languages
                entity.HasData(
                    new ProgrammingLanguage
                    {   
                        ProgrammingLanguageId = i + 1, // Start from 1
                        Name = enumAsString
                    }
                );
            }
        }
    }
}
