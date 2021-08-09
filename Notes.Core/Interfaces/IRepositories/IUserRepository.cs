using System.Threading.Tasks;
using Notes.Core.Entities;

namespace Notes.Core.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetLoginByCredentials(UserLogin login);
        Task<User> GetById(int id);
        Task Add(User user);
        void Update(User user);
        Task Delete(int id);
    }
}
