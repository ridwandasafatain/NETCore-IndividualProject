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
    public class CoursesController : ControllerBase
    {
        private readonly ICourse _course;
        private readonly IMapper _mapper;

        public CoursesController(ICourse course, IMapper mapper)
        {
            _course = course;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CourseGetDTO> Get()
        {
            var courses = _course.GetAll();
            var coursesGetDto = _mapper.Map<IEnumerable<CourseGetDTO>>(courses);
            return coursesGetDto;
        }

        [HttpGet("{id}")]
        public CourseGetDTO GetById(int id)
        {
            var course = _course.GetById(id);
            var courseGetDto = _mapper.Map<CourseGetDTO>(course);
            return courseGetDto;
        }

        [HttpGet("ByTitle")]
        public IEnumerable<CourseGetDTO> GetByName(string name)
        {
            var results = _course.GetByName(name);
            var listCourse = _mapper.Map<IEnumerable<CourseGetDTO>>(results);
            return listCourse;
        }

        [HttpGet("Student{id}")]
        public IEnumerable<CourseGetWithStudentDTO> GetCourseGetWithStudents(int id)
        {
            var result = _course.CourseGetAllStudent(id);
            var listCourse = _mapper.Map<IEnumerable<CourseGetWithStudentDTO>>(result);
            return listCourse;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _course.Delete(id);
                return Ok($"data course id {id} deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("AllStudent{id}")]
        public IActionResult DeleteAllStudent(int id)
        {
            
            try
            {
                _course.RemoveAllStudentCourse(id);
                return Ok($"Semua students dari Course id {id} berhasil dihapus");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update(CourseAddDTO courseAddDTO)
        {
            try
            {
                var course = _mapper.Map<Course>(courseAddDTO);
                var newCourse = _course.Insert(course);
                var courseGetDto = _mapper.Map<CourseGetDTO>(course);
                return CreatedAtAction("Get", new { id = courseGetDto.CourseId }, courseGetDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Enrollment")]
        public IActionResult AddEnrollment(AddEnrollmentDTO enrollmentDTO)
        {
            
            try
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentDTO);
                var newEnrollment = _course.InsertEnrollment(enrollment);
                return Ok($"Enrollment berhasil ditambahkan");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(CourseAddDTO courseAddDTO)
        {
            try
            {
                var course = _mapper.Map<Course>(courseAddDTO);
                _course.Update(course);
                var newCourse = _mapper.Map<CourseGetDTO>(course);
                return Ok(newCourse);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
