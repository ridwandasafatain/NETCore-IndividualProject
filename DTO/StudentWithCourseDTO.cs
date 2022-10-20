using IndividualProject.Models;

namespace IndividualProject.DTO
{
    public class StudentWithCourseDTO
    {
        public int Id { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        
        public List<CourseGetDTO> Courses { get; set; }
    }
}
