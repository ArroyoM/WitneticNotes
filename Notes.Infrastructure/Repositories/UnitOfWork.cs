using System.Threading.Tasks;
using Notes.Core.Interfaces;
using Notes.Core.Interfaces.IRepositories;
using Notes.Infrastructure.Data;

namespace Notes.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NotesContext _notesContext;

        private readonly IUserRepository _userRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IBookRepository _bookRepository;

        public UnitOfWork(NotesContext notesContext)
        {
            _notesContext = notesContext;
        }
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_notesContext);
        public INoteRepository NoteRespository => _noteRepository ?? new NoteRepository(_notesContext);
        public IBookRepository BookRepository => _bookRepository ?? new BookRepository(_notesContext);

        public void Dispose()
        {
            if(_notesContext != null)
            {
                _notesContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            _notesContext.SaveChanges();
        }

        public async Task SaveChangesAsysc()
        {
            await _notesContext.SaveChangesAsync();
        }
    }
}
