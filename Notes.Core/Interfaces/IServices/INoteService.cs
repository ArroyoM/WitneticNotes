using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Core.Entities;

namespace Notes.Core.Interfaces.IServices
{
    public interface INoteService
    {
        IEnumerable<Note> GetNotes(int idBook);
        Task<Note> GetNote(int id);
        Task InsertNote(Note note);
        Task<bool> UpdateNote(Note note);
        Task<bool> DeleteNote(int id);
    }
}
