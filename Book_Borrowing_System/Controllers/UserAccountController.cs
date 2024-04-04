using AutoMapper;
using BuisnessLayer.Domain;
using BuisnessLayer.BookAppServices.Interface;
using SharedLayer.Core.Utils;
using Book_Borrowing_System.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BuisnessLayer.BookAppServices.Implementation;

namespace Book_Borrowing_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        public IUserService UserService { get; }
        public IMapper Mapper { get; }
        public JWTSetting JwtSetting { get; }

        public UserAccountController(IUserService userService, IMapper mapper, IOptions<JWTSetting> options)
        {
            UserService = userService;
            Mapper = mapper;
            JwtSetting = options.Value;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(UserLoginDto user)
        {
            var result = await UserService.GetUserWithDetails(user.Username, CommonMethods.Encrypt(user.Password));
            if (result.Data == null)
                return Unauthorized();

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(JwtSetting.securitykey);
            var userClaims = new Claim[]{
                                new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()),
                                new Claim(ClaimTypes.Name, result.Data.Username),
                                new Claim("name", result.Data.Name),
                                new Claim("token", result.Data.Tokens_Available.ToString()),

                            };
            var userIdentity = new ClaimsIdentity(userClaims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = userIdentity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            return Ok(new { token = finaltoken });
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDto>> Get(int id)
        {
            var loginId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var loginUserId = int.Parse(loginId);
            if (loginUserId != id)
                return Unauthorized("You are not authorized to view this profile");
            var result = await UserService.GetUserById(id);
            if (result.IsSuccess == false || result.Data == null)
                return NotFound();
            var user = Mapper.Map<UserDto>(result.Data);
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Post(UserDto user)
        {
            user.Password = CommonMethods.Encrypt(user.Password);
            UserDomain userToCreate = Mapper.Map<UserDomain>(user);
            var result = await UserService.CreateUser(userToCreate);
            if (result.IsSuccess)
                return Created(nameof(Post), user);
            return BadRequest(result.MainMessage.Text);
        }
    }
}
