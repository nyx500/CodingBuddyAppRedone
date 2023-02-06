// Stores a separate configuration class for the User entity which is used in the OnModelCreating() method in the ApplicationDbContext class

using Microsoft.EntityFrameworkCore;
using CBApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using CBApp.Data;
using System.Linq;
using Microsoft.Extensions.Options;

namespace CBApp.Data.ConfigurationFiles
{
    internal class QuestionAnswerBlockConfig : IEntityTypeConfiguration<QuestionAnswerBlock>
    {
        // We must override the Configure method in the IEntityTypeConfiguration interface here
        public void Configure(EntityTypeBuilder<QuestionAnswerBlock> entity)
        {
            entity.HasKey(qa => new { qa.QuestionAnswerBlockId }).HasName("QuestionAnswerBlockId");
            entity.Property(qa => qa.SlackId);

            entity.Property(qa => qa.QuestionString);
            entity.Property(qa => qa.AnswerString);

            // Set up many-to-one relationships
            entity.HasOne(u => u.User).WithMany(q => q.QuestionAnswerBlocks).OnDelete(DeleteBehavior.Cascade); ;

            //entity.HasData(
            //    new QuestionAnswerBlock
            //    {
            //        QuestionAnswerBlockId = 1,
            //        SlackId = "U73VQP71",
            //        QuestionString = "What three things do you think of the most each day?",
            //        AnswerString = "Cats, coding, food"
            //    }
            //);

        }
    }
}