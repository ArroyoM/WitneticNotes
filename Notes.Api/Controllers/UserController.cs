using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Responses;
using Notes.Core.DTOs;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IServices;
using Notes.Infrastructure.Interfaces;

namespace Notes.Api.Controllers
{
    /// <summary>
    /// Controller User
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="mapper"></param>
        /// <param name="passwordService"></param>
        public UserController(IUserService userService, IMapper mapper, IPasswordService passwordService)
        {
            _userService = userService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Find user for id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                User user = await _userService.GetUser(id);

                if (user == null)
                {
                    return NotFound();
                }

                var userResponDTO = _mapper.Map<UserResponDTO>(user);
                var response = new ApiResponse<UserResponDTO>(userResponDTO);
   
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }
        }

        /// <summary>
        /// update a user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>       
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(UserDTO userDTO, int id)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                user.IdUser = id;
                user.Password = user.Password != null ?  _passwordService.Hash(user.Password) : user.Password ;

                var result = await _userService.UpdateUser(user);
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
        /// delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //TODO DELTE BOOKS
                var result = await _userService.DeleteUser(id);

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
        /// register a new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>UserResponDTO</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);

                user.Password = _passwordService.Hash(user.Password);
                await _userService.InsertUser(user);

                if (user == null)
                {
                    return BadRequest();
                }

                var userResponDTO = _mapper.Map<UserResponDTO>(user);

                return CreatedAtAction(nameof(Get), new { id = userResponDTO.IdUser }, userResponDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError<String>(ex.Message));
            }
        }
    }
}
