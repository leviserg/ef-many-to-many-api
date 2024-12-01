using students_courses_api.Models;

namespace students_courses_api.Data
{
    public class DbSeed
    {
        private readonly AppDbContext _dbContext;

        public DbSeed(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {

            var students = new List<Student>() {
                new Student { Name = "John Doe"},
                new Student { Name = "Jane Air" }
            };

            var courses = new List<Course>() {
                new Course { Name = "Angular" },
                new Course { Name = "SQL" }
            };

            _dbContext.Students.AddRange(
                students
                );
            _dbContext.Courses.AddRange(
                courses
            );
            _dbContext.StudentCourses.AddRange(
                new StudentCourse { Course = courses[0], Student = students[0] },
                new StudentCourse { Course = courses[1], Student = students[1] }
                );

            _dbContext.SaveChanges();
        }
    }
}
