// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class NaturalLanguageConfig : IEntityTypeConfiguration<NaturalLanguage>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<NaturalLanguage> entity)
        {
              entity.Property(n => n.NaturalLanguageId);
              entity.Property(n => n.Name);

            foreach (int i in Enum.GetValues(typeof(EnumsForUser.NaturalLanguageName)))
            {
                // Casts integer into the corresponding enum
                var enumLanguage = (EnumsForUser.NaturalLanguageName)i;

                // Get the language name from the enum as a string
                string enumAsString = enumLanguage.ToString();

                // Seed the DB with names and Ids of natural spoken languages
                entity.HasData(
                    new NaturalLanguage
                    {
                        // Initializes the first Id with '1' instead of '0'
                        NaturalLanguageId = (i + 1),
                        Name = enumAsString
                    }
                );
            }
        }
    }
}
