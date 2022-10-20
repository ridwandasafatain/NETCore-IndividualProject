using IndividualProject.Models;

namespace IndividualProject.DAL
{
    public interface IEnrollment
    {
        public IEnumerable<Enrollment> GetAll();
        public IEnumerable<Enrollment> GetByName(string name);
        public Enrollment GetById(int id);
        public Enrollment Insert(Enrollment enrollment);
        public Enrollment Update(Enrollment enrollment);
        public Enrollment Delete(int id);
    }
}
