using System.ComponentModel.DataAnnotations.Schema;

namespace IndividualProject.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
