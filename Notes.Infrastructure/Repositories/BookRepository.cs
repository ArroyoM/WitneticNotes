using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Infrastructure.Data;

namespace Notes.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly NotesContext _notesContext;

        public BookRepository(NotesContext notesContext)
        {
            _notesContext = notesContext;
        }

        public IEnumerable<Book> GetAll(int idUser)
        {
            return _notesContext.Set<Book>().Where(b => b.IdUser == idUser).AsEnumerable();
        }

        public async Task<Book> GetById(int id)
        {
            return await _notesContext.Set<Book>().FindAsync(id);
        }

        public async Task Add(Book book)
        {
            await _notesContext.Set<Book>().AddAsync(book);
        }

        public void Update(Book book)
        {
            _notesContext.Set<Book>().Update(book);
        }

        public async Task Delete(int id)
        {
            Book book = await GetById(id);
            _notesContext.Set<Book>().Remove(book);
        }
    }
}
