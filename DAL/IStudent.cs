using IndividualProject.Models;

namespace IndividualProject.DAL
{
    public interface IStudent
    {
        public IEnumerable<Student> GetAll();
        
        public IEnumerable<Student> GetByName(string name);
        public Student GetById(int id);
        public Student Insert(Student student);
        public Student Update(Student student);
        public void Delete(int id);


        public IEnumerable<Student> GetAllWithCourse();
        public IEnumerable<Student> GetStudentWithCourse(int id);
        public void AddStudentToCourse(int studentId, int courseId);
        public void AddNewStudentToCourse(int courseId, Student student);
        public void RemoveStudentFromCourse(int studentId, int courseId);
        //public void RemoveAllStudentCourse(int courseId);
    }
}
