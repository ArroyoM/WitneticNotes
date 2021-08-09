using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Core.Entities;

namespace Notes.Core.Interfaces.IRepositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll(int idUser);
        Task<Book> GetById(int id);
        Task Add(Book book);
        void Update(Book book);
        Task Delete(int id);
    }
}
