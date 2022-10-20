using IndividualProject.Models;

namespace IndividualProject.DAL
{
    public interface ICourse
    {
        public IEnumerable<Course> GetAll();
        public IEnumerable<Course> GetByName(string name);
        public Course GetById(int id);
        public Course Insert(Course course);
        public Course Update(Course course);
        public void Delete(int id);

        public IEnumerable<Course> CourseGetAllStudent(int id);
        public void RemoveAllStudentCourse(int id);
        public Enrollment InsertEnrollment(Enrollment enrollment);
    }
}
