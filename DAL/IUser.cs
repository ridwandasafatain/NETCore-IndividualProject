using IndividualProject.DTO;

namespace IndividualProject.DAL
{
    public interface IUser
    {
        Task Registration(AddUserDTO user);
        IEnumerable<UserGetDTO> GetUsers();
        Task<UserGetDTO>Authenticate(AddUserDTO user);
    }
}
