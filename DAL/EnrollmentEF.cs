using IndividualProject.Models;

namespace IndividualProject.DAL
{
    public class EnrollmentEF : IEnrollment
    {
        private readonly AppDbContext _dbcontext;

        public EnrollmentEF(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public Enrollment Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Enrollment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Enrollment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Enrollment> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Enrollment Insert(Enrollment enrollment)
        {
            throw new NotImplementedException();
        }

        public Enrollment Update(Enrollment enrollment)
        {
            throw new NotImplementedException();
        }
    }
}
