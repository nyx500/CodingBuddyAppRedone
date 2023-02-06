// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBApp.Data.ConfigurationFiles
{
    internal class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<Question> entity)
        {
            entity.HasKey(q => q.QuestionId).HasName("QuestionId");
            entity.HasIndex(q => q.QuestionId).IsUnique();

            entity.Property(n => n.QuestionString);

            //entity.HasData(
            //        new Question
            //        {   QuestionId = 1,
            //            QuestionString = "What is your dream job?"
            //        },
            //        new Question
            //        {
            //            QuestionId = 2,
            //            QuestionString = "What is your biggest fear?"
            //        },
            //        new Question
            //        {
            //            QuestionId = 3,
            //            QuestionString = "What did you want to be when you were small?"
            //        },
            //        new Question
            //        {
            //            QuestionId = 4,
            //            QuestionString = "If you could only eat one meal for the rest of your life, what would it be?"
            //        },
            //        new Question
            //        {   
            //            QuestionId = 5,
            //            QuestionString = "If you were a super-hero, what powers would you have?"
            //        },
            //        new Question
            //        {   
            //            QuestionId = 6,
            //            QuestionString = "If you could go back in time to change one thing, what would it be?"
            //        },
            //        new Question
            //        {
            //            QuestionId = 7,
            //            QuestionString = "What was the last book you read?"
            //        },
            //        new Question
            //        {
            //            QuestionId = 8,
            //            QuestionString = "What is your favorite childhood memory?"
            //        },
            //        new Question
            //        {
            //            QuestionId = 9,
            //            QuestionString = "Which of the five senses would you say is your strongest?"
            //        },
            //        new Question
            //        {
            //            QuestionId = 10,
            //            QuestionString = "What three things do you think of the most each day?"
            //        }
            //    );
        }
    }
}
