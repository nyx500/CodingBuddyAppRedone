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

            entity.HasData(
                    new Question
                    {
                        QuestionId = 1,
                        QuestionString = "What is your dream job?"
                    },
                    new Question
                    {
                        QuestionId = 2,
                        QuestionString = "What is your biggest fear?"
                    },
                    new Question
                    {
                        QuestionId = 3,
                        QuestionString = "What did you want to be when you were a kid?"
                    },
                    new Question
                    {
                        QuestionId = 4,
                        QuestionString = "If you could only eat one meal for the rest of your life, what would it be?"
                    },
                    new Question
                    {
                        QuestionId = 5,
                        QuestionString = "If you were a super-hero, what one power would you have?"
                    },
                    new Question
                    {
                        QuestionId = 6,
                        QuestionString = "If you could go back in time to change one thing, what would it be?"
                    },
                    new Question
                    {
                        QuestionId = 7,
                        QuestionString = "What was the last book you read?"
                    },
                    new Question
                    {
                        QuestionId = 8,
                        QuestionString = "What is your favorite childhood memory?"
                    },
                    new Question
                    {
                        QuestionId = 9,
                        QuestionString = "Which of the five senses would you say is your strongest?"
                    },
                    new Question
                    {
                        QuestionId = 10,
                        QuestionString = "Which three things do you think of the most each day?"
                    },
                    new Question
                    {
                        QuestionId = 11,
                        QuestionString = "What is your favourite song?"
                    },
                    new Question
                    {
                        QuestionId = 12,
                        QuestionString = "Who is your role model?"
                    },
                    new Question
                    {
                        QuestionId = 13,
                        QuestionString = "What's your most hated form of transportation?"
                    },
                    new Question
                    {
                        QuestionId = 14,
                        QuestionString = "What part of a kid’s movie completely scarred you for life?"
                    },
                    new Question
                    {
                        QuestionId = 15,
                        QuestionString = "What's the most disgusting food you've ever eaten?"
                    },
                    new Question
                    {
                        QuestionId = 16,
                        QuestionString = "Do you like tacos?"
                    },
                    new Question
                    {
                        QuestionId = 17,
                        QuestionString = "What do you think would make an awesome name for a Wi-Fi network?"
                    },
                    new Question
                    {
                        QuestionId = 18,
                        QuestionString = "If you could buy any computer regardless of cost, which one would you go for?"
                    },
                    new Question
                    {
                        QuestionId = 19,
                        QuestionString = "What is better: C# or Java?"
                    },
                    new Question
                    {
                        QuestionId = 20,
                        QuestionString = "What is the most annoying bug you ever found when coding?"
                    },
                    new Question
                    {
                        QuestionId = 21,
                        QuestionString = "Which conspiracy theory do you find particularly ridiculous?"
                    },
                    new Question
                    {
                        QuestionId = 22,
                        QuestionString = "Is Starbucks worth the money?"
                    },
                    new Question
                    {
                        QuestionId = 23,
                        QuestionString = "Type an unpopular opinion you have."
                    },
                    new Question
                    {
                        QuestionId = 24,
                        QuestionString = "Do you know your personality type?"
                    },
                    new Question
                    {
                        QuestionId = 25,
                        QuestionString = "What do you like to do best to relax?"
                    },
                    new Question
                    {
                        QuestionId = 26,
                        QuestionString = "Name one of your guilty pleasures."
                    },
                    new Question
                    {
                        QuestionId = 27,
                        QuestionString = "What is your favourite outfit?"
                    },
                    new Question
                    {
                        QuestionId = 28,
                        QuestionString = "What is the first thing you would buy if you won the lottery?"
                    },
                    new Question
                    {
                        QuestionId = 29,
                        QuestionString = "What is something everyone seems to love that you hate?"
                    },
                    new Question
                    {
                        QuestionId = 30,
                        QuestionString = "What is your favourite animal?"
                    }
                );
        }
    }
}
