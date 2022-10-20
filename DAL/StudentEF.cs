using IndividualProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IndividualProject.DAL
{
    public class StudentEF : IStudent
    {
        private AppDbContext _dbcontext;

        public StudentEF(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void AddNewStudentToCourse(int courseId, Student student)
        {
            try
            {
                var newStudent = _dbcontext.Students.Add(student);
                var course = _dbcontext.Courses.FirstOrDefault(c => c.CourseId == courseId);
                if (course != null)
                {
                    course.Students.Add(student);
                    _dbcontext.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public void AddStudentToCourse(int studentId, int courseId)
        {
            try
            {
                var student = _dbcontext.Students.FirstOrDefault(s => s.Id == studentId);
                var course = _dbcontext.Courses.FirstOrDefault(c => c.CourseId == courseId);
                if(student != null && course != null)
                {
                    course.Students.Add(student);
                    _dbcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            var deleteStudent = GetById(id);
            if (deleteStudent == null)
            {
                throw new Exception($"Data id {id} tidak ditemukan"); 
            }
            try
            {
                _dbcontext.Students.Remove(deleteStudent);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Student> GetAll()
        {
            var results = _dbcontext.Students.OrderBy(s => s.Id).ToList();
            return results;
        }

        private static IEnumerable<T> PagedIterator<T>(IEnumerable<T> objectList, int PageSize)
        {
            var page = 0;
            var recordCount = objectList.Count();
            var pageCount = (int)((recordCount + PageSize) / PageSize);

            if (recordCount < 1)
            {
                yield break;
            }

            while (page < pageCount)
            {
                var pageData = objectList.Skip(PageSize * page).Take(PageSize).ToList();

                foreach (var rd in pageData)
                {
                    yield return rd;
                }
                page++;
            }
        }
        public IEnumerable<Student> GetAllWithCourse()
        {
            var results = _dbcontext.Students.Include(s=>s.Courses).ToList();
            foreach (var author in PagedIterator(results, 10))
            {
                // Do Stuff
            }
            return results;
        }

        public Student GetById(int id)
        {
            var student = _dbcontext.Students.FirstOrDefault(s => s.Id == id);
            return student;
        }

        public IEnumerable<Student> GetByName(string name)
        {
            var students = _dbcontext.Students.Where(s => s.FirstMidName.Contains(name)||s.LastName.Contains(name));
            return students;
        }

        public IEnumerable<Student> GetStudentWithCourse(int id)
        {
            var results = _dbcontext.Students.Include(s => s.Courses).Where(s=>s.Id==id).ToList();
            return results;
        }

        public Student Insert(Student student)
        {
            try
            {
                _dbcontext.Students.Add(student);
                _dbcontext.SaveChanges();
                return student;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //public void RemoveAllStudentCourse(int courseId)
        //{
        //    try
        //    {
        //        var studentWithCourse = _dbcontext.Students.Include(s => s.Courses.Where(c => c.CourseId == courseId));
        //        var student = studentWithCourse.Courses[0];
        //        List<student>
        //        studentWithCourse.Courses.Remove(student);
        //        _dbcontext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}

        public void RemoveStudentFromCourse(int studentId, int courseId)
        {
            try
            {
                var studentWithCourse = _dbcontext.Students.Include(s=>s.Courses.Where(c=>c.CourseId==courseId)).FirstOrDefault(s => s.Id == studentId);
                var student = studentWithCourse.Courses[0];
                studentWithCourse.Courses.Remove(student);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Student Update(Student student)
        {
            try
            {
                var studentUpdate = GetById(student.Id);
                studentUpdate.LastName = student.LastName;
                studentUpdate.FirstMidName = student.FirstMidName;
                _dbcontext.SaveChanges();

                return studentUpdate;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
