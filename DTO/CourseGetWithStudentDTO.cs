namespace IndividualProject.DTO
{
    public class CourseGetWithStudentDTO
    {
        public string Title { get; set; }
        public IEnumerable<StudentGetDTO> Students { get; set; }
    }
}
