using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Infrastructure.Data;

namespace Notes.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NotesContext _notesContext;

        public UserRepository(NotesContext notesContext)
        {
            _notesContext = notesContext;
        }

        public async Task<User> GetById(int id)
        {
         return await _notesContext.Set<User>().FindAsync(id);
        }

        public async Task Add(User user)
        {
            await _notesContext.Set<User>().AddAsync(user);
        }

        public void Update(User user)
        {
            _notesContext.Set<User>().Update(user);
        }

        public async Task Delete(int id)
        {
            User user =  await GetById(id);

            _notesContext.Set<User>().Remove(user);
        }

        public  async Task<User> GetLoginByCredentials(UserLogin login)
        {
              return await _notesContext.Set<User>().FirstOrDefaultAsync(x => x.Email == login.Email);
        }
    }
}
