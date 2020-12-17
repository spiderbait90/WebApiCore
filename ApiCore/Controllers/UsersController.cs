using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCore.Data;
using ApiCore.Data.EntityModels;
using ApiCore.Data.ViewModels;
using ApiCore.Helpers;
using ApiCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(StoreDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;

        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterUserModel model)
        {
            var user = _mapper.Map<User>(model);

            try
            {
                await _userService.Create(user, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = Message.UsernamePasswordIncorrect });

            var tokenString = _userService.GetToken(user);

            return Ok(new
            {
                user.Id,
                user.Username,
                Token = tokenString
            });
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserViewModel>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var usersModels = _mapper.Map<List<User>, List<GetUserViewModel>>(users);

            return usersModels;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserViewModel>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var modelUser = _mapper.Map<GetUserViewModel>(user);

            return modelUser;
        }
    }
}
