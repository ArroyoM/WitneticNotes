using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Infrastructure.Data;

namespace Notes.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesContext _notesContext;

        public NoteRepository(NotesContext noteContext)
        {
            _notesContext = noteContext;
        }

        public  IEnumerable<Note> GetAll(int idBook)
        {
            return  _notesContext.Set<Note>().Where(x => x.IdBook == idBook).AsEnumerable();
        }

        public async Task<Note> GetById(int id)
        {
            return await _notesContext.Set<Note>().FindAsync(id);
        }

        public async Task Add(Note note)
        {
            await _notesContext.Set<Note>().AddAsync(note);
        }

        public void Update(Note note)
        {
            _notesContext.Set<Note>().Update(note);
        }

        public async Task Delete(int id)
        {
            Note note = await GetById(id);
            _notesContext.Set<Note>().Remove(note);
        }
    }
}
