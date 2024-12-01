using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using students_courses_api.Data;
using students_courses_api.Models;

namespace students_courses_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentCoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses()
        {
            return await _context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .Select(e => new StudentCourse {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    Student = e.Student,
                    Course = e.Course,
                    LastChanged = e.LastChanged
                })
                .ToListAsync();
        }

        // GET: api/StudentCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCourse>> GetStudentCourse(int id)
        {
            var studentCourse = await _context.StudentCourses
                .Include(sc => sc.Course)
                .Include(sc => sc.Student)
                .Select(e => new StudentCourse {
                        Id = e.Id,
                        StudentId = e.StudentId,
                        CourseId = e.CourseId,
                        Student = e.Student,
                        Course = e.Course,
                        LastChanged = e.LastChanged
                    })
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (studentCourse == null)
            {
                return NotFound();
            }

            return studentCourse;
        }

        // PUT: api/StudentCourses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentCourse(int id, StudentCourse studentCourse)
        {
            if (id != studentCourse.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(studentCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentCourses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> PostStudentCourse(StudentCourse studentCourse)
        {
            _context.StudentCourses.Add(studentCourse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentCourseExists(studentCourse.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentCourse", new { id = studentCourse.StudentId }, studentCourse);
        }

        // DELETE: api/StudentCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentCourse(int id)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentCourseExists(int id)
        {
            return _context.StudentCourses.Any(e => e.StudentId == id);
        }
    }
}
