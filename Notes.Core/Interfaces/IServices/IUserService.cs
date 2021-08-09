using System.Threading.Tasks;
using Notes.Core.Entities;

namespace Notes.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<User> GetUser(int id);
        Task InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<User> GetLoginByCredentials(UserLogin userLogin);
    }
}
