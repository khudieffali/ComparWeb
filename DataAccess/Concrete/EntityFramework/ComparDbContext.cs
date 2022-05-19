using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class ComparDbContext:IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=ComparWebDB;Trusted_Connection=true;MultipleActiveResultSets=true");
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<CourseTopic> CourseTopics { get; set; }
        public DbSet<CourseToSkill> CourseToSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<ArticleToTag> ArticleToTags { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users");
        }
    }
}
