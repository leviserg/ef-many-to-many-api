using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace students_courses_api.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }


        [DataType(DataType.DateTime)]
        public  DateTime LastChanged { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
