// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CBApp.Data.ConfigurationFiles
{
    internal class CareerPhaseConfig : IEntityTypeConfiguration<CareerPhase>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<CareerPhase> entity)
        {
            entity.Property(n => n.CareerPhaseId);
            entity.Property(n => n.Name);

            //foreach (int i in Enum.GetValues(typeof(EnumsForUser.CareerPhase)))
            //{
            //    // Casts integer into the corresponding enum
            //    var enumLanguage = (EnumsForUser.CareerPhase)i;

            //    // Get the CareerPhase name from the enum as a string
            //    string enumAsString = enumLanguage.ToString();

            //    // Seed the DB with names and Ids of different CareerPhases
            //    entity.HasData(
            //        new CareerPhase
            //        {
            //            // Initializes the first Id value with '1' instead of '0'
            //            CareerPhaseId = (i + 1),
            //            Name = enumAsString
            //        }
            //    );
            //}
        }
    }
}
