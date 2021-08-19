using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IServices;
using Notes.Infrastructure.Interfaces;

namespace Notes.Api.Controllers
{
    /// <summary>
    /// Controller token
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="userService">IUserService</param>
        /// <param name="passwordService">IPasswordService</param>
        public TokenController(IConfiguration configuration, IUserService userService, IPasswordService passwordService)
        {
            _configuration = configuration;
            _userService = userService;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Authentication user
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Token</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            try
            {
                //if it is a valid user
                var validation = await IsValidUser(login);

                if (validation.Item1)
                {
                    var token = GenerateToken(validation.Item2);
                    return Ok(new { 
                        tokenJwt = token,
                        email = validation.Item2.Email,
                        name = validation.Item2.Name,
                        id = validation.Item2.IdUser
                    });
                }

                return NotFound();
            } catch (Exception ex)
            {
                //TODO created LogException(e);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// valid if the user is valid
        /// </summary>
        /// <param name="login">UserLoginDTO</param>
        /// <returns>item(bool, User)</returns>
        private async Task<(bool, User)> IsValidUser(UserLogin login)
        {
            var user = await _userService.GetLoginByCredentials(login);    
            
            if(user == null)
            {
                return (false,null);
            }
            var isValid = _passwordService.Check(user.Password, login.Password);

            return (isValid, user);
        }

        /// <summary>
        /// Generation JWT for user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>String</returns>
        private string GenerateToken(User user)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(120)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
