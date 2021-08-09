using Notes.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        INoteRepository NoteRespository { get; }
        IBookRepository BookRepository { get; }
        IUserRepository UserRepository { get;  }
        void SaveChanges();

        Task SaveChangesAsysc();
    }
}
