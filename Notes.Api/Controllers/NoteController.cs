using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Notes.Api.Responses;
using Notes.Core.DTOs;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IServices;
using System.Collections.Generic;

namespace Notes.Api.Controllers
{
    /// <summary>
    /// Controller of Notes
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="noteService"></param>
        /// <param name="mapper"></param>
        public NoteController(INoteService noteService, IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        /// <summary>
        /// Return all notes of a book
        /// </summary>
        /// <param name="idBook"></param>
        /// <returns>List NoteDTO</returns>
        [HttpGet("{idBook}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult All(int idBook)
        {
            try
            {
                var notes =  _noteService.GetNotes(idBook);

                if (notes == null)
                {
                    return NotFound();
                }

                var noteDTO = _mapper.Map<IEnumerable<NoteDTO>>(notes);

                var result = new ApiResponse<IEnumerable<NoteDTO>>(noteDTO);
                return Ok(result);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }
        }

        /// <summary>
        /// Created a note
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns>NoteDTO</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(NoteDTO noteDTO)
        {
            try
            {
                var note = _mapper.Map<Note>(noteDTO);
                await _noteService.InsertNote(note);

                if (note == null)
                {
                    return BadRequest();
                }

                noteDTO = _mapper.Map<NoteDTO>(note);
                var response = new ApiResponse<NoteDTO>(noteDTO);
                return Ok(response);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }

        }

        /// <summary>
        /// Update a note
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <param name="id"></param>
        /// <returns>NoteDTO</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(NoteDTO noteDTO , int id)
        {
            try
            {
                var note = _mapper.Map<Note>(noteDTO);
                note.IdNote = id;

                var result = await _noteService.UpdateNote(note);
                if (!result)
                {
                    return NotFound();
                }
                var response = new ApiResponse<bool>(result);

                return Ok(response);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }

        }

        /// <summary>
        /// Delete a Note for Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ApiResponse T </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _noteService.DeleteNote(id);
                if (!result)
                {
                    return NotFound();
                }
                var response = new ApiResponse<bool>(result);
                return Ok(response);

            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }
        }
    }
}
