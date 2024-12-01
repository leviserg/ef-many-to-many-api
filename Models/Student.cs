using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace students_courses_api.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName="nvarchar(50)")]
        public string Name { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime LastChanged { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
