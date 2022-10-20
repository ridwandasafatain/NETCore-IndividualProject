using IndividualProject.Models;

namespace IndividualProject.DTO
{
    public class AddEnrollmentDTO
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grade? Grade { get; set; }
    }
}
