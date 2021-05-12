using Microsoft.EntityFrameworkCore;
using midTerm.Data.Entities;

namespace midTerm.Data
{
    public class MidTermDbContext  
        : DbContext
    {
        public MidTermDbContext(DbContextOptions<MidTermDbContext> options) 
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<SurveyUser> SurveyUsers { get; set; }
        public DbSet<Answers> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(question =>
            {
                question.Property(q => q.Id).IsRequired();
                question.Property(q => q.Text).HasMaxLength(500).IsRequired();
                question.Property(q => q.Text).HasMaxLength(1000).IsRequired();
                question.HasKey(q => q.Id);
                question.HasMany(q => q.Options);
            });

            modelBuilder.Entity<Option>(option =>
            {
                option.Property(o => o.Id).IsRequired();
                option.Property(o => o.Text).IsRequired();
                option.Property(o => o.QuestionId).IsRequired();
                option.HasOne(o => o.Question);
                option.HasKey(o => o.Id);
            });

            modelBuilder.Entity<Answers>(answer =>
            {
                answer.Property(a => a.Id).IsRequired();
                answer.Property(a => a.OptionId).IsRequired();
                answer.HasKey(a => a.Id);
                answer.HasOne(a => a.Option);
            });

            modelBuilder.Entity<SurveyUser>(surveyUser =>
            {
                surveyUser.Property(su => su.Id).IsRequired();
                surveyUser.Property(su => su.FirstName).IsRequired();
                surveyUser.Property(su => su.Gender).IsRequired();
                surveyUser.HasKey(su => su.Id);

            });
        }

    }
}
