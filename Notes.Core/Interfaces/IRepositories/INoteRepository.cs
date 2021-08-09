using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Core.Entities;

namespace Notes.Core.Interfaces.IRepositories
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetAll(int idBook);
        Task<Note> GetById(int id);
        Task Add(Note note);
        void Update(Note note);
        Task Delete(int id);
    }
}
