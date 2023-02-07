// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class HobbyConfig : IEntityTypeConfiguration<Hobby>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<Hobby> entity)
        {

            entity.Property(n => n.HobbyId);
            entity.Property(n => n.Name);

            foreach (int i in Enum.GetValues(typeof(EnumsForUser.HobbyName)))
            {
                // Casts integer into the corresponding enum
                var enumLanguage = (EnumsForUser.HobbyName)i;

                string enumAsString = enumLanguage.ToString();

                // Seed the DB with names and Ids of hobbies
                entity.HasData(
                    new Hobby
                    {
                        // Initializes the first Id with '1' instead of '0'
                        HobbyId = (i + 1),
                        Name = enumAsString
                    }
                );
            }
        }
    }
}
