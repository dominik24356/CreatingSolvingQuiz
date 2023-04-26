using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizGen.Model.Question;
using QuizGen.Model.Quiz;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace QuizGen.Database
{
    public class MyDbContext : DbContext
    {
    
        public DbSet<Question> questions { get; set; }
        public DbSet<Quiz> quizzes { get;  set; }

        public string path = @"D:\bazaWPF\quiz.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data source{path}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                . HasMany(q => q.Questions)
                .WithOne(q => q.Quiz)
                 .HasForeignKey(q => q.QuizId);

        }


    }
}
