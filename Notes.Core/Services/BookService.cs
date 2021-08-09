using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Core.Entities;
using Notes.Core.Interfaces;
using Notes.Core.Interfaces.IServices;

namespace Notes.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Book> GetBooks(int idUser)
        {
            try
            {
                return _unitOfWork.BookRepository.GetAll(idUser);
            }
            catch (Exception)
            {
                throw new Exception("Error in server get all book");
            }
        }

        public async Task<Book> GetBook(int id)
        {
            try
            {
                return await _unitOfWork.BookRepository.GetById(id);
            }catch(Exception)
            {
                throw new Exception("Error in server get book");
            }
        }

        public async Task InsertBook(Book book)
        {
            try
            {
                book.Created_time = DateTime.Now;
                book.Updated_time = DateTime.Now;

                await _unitOfWork.BookRepository.Add(book);
                await _unitOfWork.SaveChangesAsysc();

            }catch(Exception)
            {
                throw new Exception("Error in insert book ");
            }
        }

        public async Task<bool> UpdateBook(Book book)
        {
            try
            {
                var exitingBook = await _unitOfWork.BookRepository.GetById(book.IdBook);

                if (exitingBook == null)
                {
                    return false;
                }

                exitingBook.Name = book.Name;
                exitingBook.Color = book.Color;
                exitingBook.Description = book.Description;
                _unitOfWork.BookRepository.Update(exitingBook);
                await _unitOfWork.SaveChangesAsysc();
                return true;
            }
            catch(Exception)
            {
                throw new Exception("Error in server update book");
            }

        }

        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                await _unitOfWork.BookRepository.Delete(id);
                await _unitOfWork.SaveChangesAsysc();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}
