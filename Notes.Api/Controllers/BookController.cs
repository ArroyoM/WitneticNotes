using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Responses;
using Notes.Core.DTOs;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IServices;

namespace Notes.Api.Controllers
{

    /// <summary>
    /// Controller of Books
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="bookService">IBookService</param>
        /// <param name="mapper">IMapper</param>
        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        /// <summary>
        /// return all books
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet("user/{idUser}")]
        public IActionResult All(int idUser)
        {
            //TODO GES ALL BOOK OF USER
            try
            {
                var books = _bookService.GetBooks(idUser);

                if (books == null)
                {
                    return NotFound();
                }

                var bookDTO = _mapper.Map<IEnumerable<BookDTO>>(books);

                var result = new ApiResponse<IEnumerable<BookDTO>>(bookDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }
        }

        /// <summary>
        /// return a boook
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var book = await _bookService.GetBook(id);

                if (book == null)
                {
                    return NotFound();
                }

                var bookDTO = _mapper.Map<BookDTO>(book);

                var result = new ApiResponse<BookDTO>(bookDTO);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }

        }

        /// <summary>
        /// Created a book
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns>Book information created</returns>
        [HttpPost]
        public async Task<IActionResult> Post(BookDTO bookDTO)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDTO);
                await _bookService.InsertBook(book);

                if (book == null)
                {
                    return BadRequest();
                }

                bookDTO = _mapper.Map<BookDTO>(book);

                return CreatedAtAction(nameof(Get), new { id = bookDTO.IdBook }, bookDTO);

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }

        }

        /// <summary>
        /// Update a book for Id
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <param name="id"></param>
        /// <returns>BookDTO</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(BookDTO bookDTO, int id)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDTO);
                book.IdBook = id;

                var result = await _bookService.UpdateBook(book);

                if (!result)
                {
                    return NotFound();
                }

                var response = new ApiResponse<bool>(result);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }

        }

        /// <summary>
        /// Delete a book for Id
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>response T </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //TODO DELETE NOTES
                var result = await _bookService.DeleteBook(id);
                if (!result)
                {
                    return NotFound();
                }
                var response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }

        }
    }
}
