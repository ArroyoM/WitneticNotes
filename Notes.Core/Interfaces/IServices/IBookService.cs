using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Core.Entities;

namespace Notes.Core.Interfaces.IServices
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks(int idUser);
        Task<Book> GetBook(int id);
        Task InsertBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(int id);
    }
}
