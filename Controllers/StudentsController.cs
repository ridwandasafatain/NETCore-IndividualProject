using AutoMapper;
using IndividualProject.DAL;
using IndividualProject.DTO;
using IndividualProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndividualProject.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudent _student;
        private readonly IMapper _mapper;

        public StudentsController(IStudent student, IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<StudentGetDTO> Get()
        {
            var students = _student.GetAll();
            var studentGetDto = _mapper.Map<IEnumerable<StudentGetDTO>>(students);
            return studentGetDto;
        }

        [HttpGet("{id}")]
        public StudentGetDTO Get(int id)
        {
            var result = _student.GetById(id);
            var student = _mapper.Map<StudentGetDTO>(result);
            return student;
        }

        [HttpGet("GetByName")]
        public IEnumerable<StudentGetDTO> GetByName(string name)
        {
            var results = _student.GetByName(name);
            var students = _mapper.Map<IEnumerable<StudentGetDTO>>(results);
            return students;
        }

        [HttpGet("WithCourse")]
        public IEnumerable<StudentWithCourseDTO> StudentWithCourses()
        {
            var result = _student.GetAllWithCourse();
            var students = _mapper.Map<IEnumerable<StudentWithCourseDTO>>(result);
            return students;
        }

        [HttpGet("Course{id}")]
        public IEnumerable<StudentWithCourseDTO> GetIdWithCourses(int id)
        {
            var result = _student.GetStudentWithCourse(id);
            var students = _mapper.Map<IEnumerable<StudentWithCourseDTO>>(result);
            return students;
        }

        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _student.Delete(id);
                return Ok($"Data id {id} berhasil di hapus");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(StudentAddDTO studentDto)
        {
            try
            {
                var student = _mapper.Map<Student>(studentDto);
                var newStudent = _student.Insert(student);
                var studentGetDto = _mapper.Map<StudentGetDTO>(newStudent);
                return CreatedAtAction("Get", new { id = student.Id }, studentGetDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Course")]
        public IActionResult AddStudentToCourse(AddStudentToCourseDTO addStudentToCourse)
        {
            try
            {
                _student.AddStudentToCourse(addStudentToCourse.StudentId, addStudentToCourse.CourseId);
                return Ok($"Berhasil menambahkan student id {addStudentToCourse.StudentId} ke course {addStudentToCourse.CourseId}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
                
        [HttpPost("NewStudent")]
        public IActionResult AddNewStudentToCourse(AddNewStudentToCourseDTO newStudentToCourse)
        {
            try
            {
                var student = _mapper.Map<Student>(newStudentToCourse);
                var newStudent = _student.Insert(student);
                var studentGetDto = _mapper.Map<StudentWithCourseDTO>(newStudent);
                _student.AddStudentToCourse(newStudent.Id, newStudentToCourse.CourseId);
                return Ok($"Berhasil Menambah Student {newStudent.FirstMidName} {newStudent.LastName} ke Course Id {newStudentToCourse.CourseId}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(StudentWithIdAddDTO studentDto)
        {
            try
            {
                var student = _mapper.Map<Student>(studentDto);
                _student.Update(student);
                var newStudent = _mapper.Map<StudentGetDTO>(student);
                return Ok(newStudent);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RemoveCourse")]
        public IActionResult RemoveStudentFromCourse(AddStudentToCourseDTO removeStudent)
        {

            try
            {
                _student.RemoveStudentFromCourse(removeStudent.StudentId, removeStudent.CourseId);
                return Ok($"Deleted Student {removeStudent.StudentId} from {removeStudent.CourseId}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete("Course{courseId}")]
        //public IActionResult RemoveAllStudentFromCourse(int courseId)
        //{
        //    try
        //    {
        //        _student.RemoveAllStudentCourse(courseId);
        //        return Ok($"Semua student dari course id {courseId} berhasil didelete");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}
