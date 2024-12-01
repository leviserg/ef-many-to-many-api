using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace students_courses_api.Models
{
    public class StudentCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }


        [ForeignKey(nameof(StudentId))]
        public required Student Student { get; set; }


        [ForeignKey(nameof(CourseId))]
        public required Course Course { get; set; }
        public DateTime LastChanged { get; set; }
    }
}
