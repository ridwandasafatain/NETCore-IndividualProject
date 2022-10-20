using IndividualProject.DTO;
using IndividualProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IndividualProject.DAL
{
    public class CourseEF : ICourse
    {
        private readonly AppDbContext _dbcontext;

        public CourseEF(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public IEnumerable<Course> CourseGetAllStudent(int id)
        {
            var results = _dbcontext.Courses.Include(c => c.Students).Where(c => c.CourseId == id);
            return results;
        }

        public void Delete(int id)
        {
            try
            {
                var deleteCourse = GetById(id);
                _dbcontext.Courses.Remove(deleteCourse);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Course> GetAll()
        {
            var courses = _dbcontext.Courses.OrderBy(c => c.CourseId).ToList();
            return courses;
        }

        public Course GetById(int id)
        {
            var course = _dbcontext.Courses.FirstOrDefault(c => c.CourseId == id);
            return course;
        }

        public IEnumerable<Course> GetByName(string name)
        {
            var courses = _dbcontext.Courses.Where(c => c.Title.Contains(name));
            return courses;
        }

        public Course Insert(Course course)
        {
            try
            {
                _dbcontext.Courses.Add(course);
                _dbcontext.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Enrollment InsertEnrollment(Enrollment enrollment)
        {
            try
            {
                _dbcontext.Enrollments.Add(enrollment);
                _dbcontext.SaveChanges();
                return enrollment;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void RemoveAllStudentCourse(int id)
        {
            try
            {
                var results = CourseGetAllStudent(id);
                _dbcontext.Courses.Remove((Course)results);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Course Update(Course course)
        {
            try
            {
                var update = GetById(course.CourseId);
                update.Title = course.Title;
                update.Credits = course.Credits;
                _dbcontext.SaveChanges();
                return update;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
