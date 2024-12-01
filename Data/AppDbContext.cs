using Microsoft.EntityFrameworkCore;
using students_courses_api.Models;

namespace students_courses_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .Property(p => p.LastChanged)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Course>()
                .Property(p => p.LastChanged)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<StudentCourse>()
                .Property(p => p.LastChanged)
                .HasDefaultValueSql("GETUTCDATE()");


            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

           
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

        }

    }
}
