using Microsoft.Data.Sqlite;
using midTerm.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using midTerm.Data.Entities;

namespace midTerm.Service.Test.Internal
{
    public abstract class SqlLiteContext : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;
        protected readonly MidTermDbContext DbContext;

        protected DbContextOptions<MidTermDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<MidTermDbContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlite(_connection)
                .Options;
        }

        protected SqlLiteContext(bool withData = false)
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            DbContext = new MidTermDbContext(CreateOptions());
            _connection.Open();
            DbContext.Database.EnsureCreated();
            if (withData)
                SeedData(DbContext);
        }

        private void SeedData(MidTermDbContext context)
        {
            var answer = new List<Answers>
            {
                new Answers
                {
                   Id = 1,
                   OptionId = 3,
                   UserId = 1
                },

                new Answers
                {
                   Id = 2,
                   OptionId = 2,
                   UserId = 1
                },

                new Answers
                {
                   Id = 3,
                   OptionId = 1,
                   UserId = 1
                }
            };

            var option = new List<Option>
            {
                new Option
                {
                    Id = 1,
                    Order = 1,
                    QuestionId = 1,
                    Text = "Too Long"
                },

                new Option
                {
                    Id = 2,
                    Order = 2,
                    QuestionId = 1,
                    Text = "Mitochondria"
                },

                new Option
                {
                    Id = 3,
                    Order = 1,
                    QuestionId = 2,
                    Text = "Watson"                   
                }
            };

            var question = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    Text = "How long will the exam take?",
                    Description = "No description necessary.",
                },
                new Question
                {
                    Id = 2,
                    Text = "What was the name of Sherlock's Assistant?",
                    Description = "Sherlock being Sherlock Holmes in this case.",
                },
                new Question
                {
                    Id = 3,
                    Text = "Where is the Eiffel Tower located?",
                    Description = "No description necessary.",
                }
            };

            var user = new List<SurveyUser>
            {
                new SurveyUser
                {
                   Id = 1,
                   FirstName = "Mark",
                   LastName = "Ruzinov",
                   Gender = Data.Enums.Gender.Male
                }
            };
            context.AddRange(option);
            context.AddRange(question);
            context.AddRange(answer);
            context.AddRange(user);
            context.SaveChanges();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
