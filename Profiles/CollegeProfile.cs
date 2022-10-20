using AutoMapper;
using IndividualProject.DTO;
using IndividualProject.Models;

namespace IndividualProject.Profiles
{
    public class CollegeProfile : Profile
    {
        public CollegeProfile()
        {
            CreateMap<Student, StudentGetDTO>();
            //CreateMap<Student, StudentWithIdAddDTO>();
            CreateMap<StudentWithIdAddDTO, Student>();
            CreateMap<StudentAddDTO, Student>();
            CreateMap<Student, StudentWithCourseDTO>();
            CreateMap<AddNewStudentToCourseDTO, Student>();

            CreateMap<Course, CourseGetDTO>();
            CreateMap<CourseAddDTO,Course>();
            CreateMap<Course, CourseGetWithStudentDTO>();
            CreateMap<RemoveStudentsFromCourse,Course>();

            CreateMap<AddEnrollmentDTO,Enrollment>();
        }
    }
}
