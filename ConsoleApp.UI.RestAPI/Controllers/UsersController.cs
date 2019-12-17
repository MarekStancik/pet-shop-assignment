using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShopApp.Core.DomainService;
using PetShopApp.Core.Entities;
using PetShopApp.Core.Helpers;

namespace ConsoleApp.UI.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserRepository _userRepository;
        readonly IAuthenticationHelper _authenticationHelper;
        
        public UsersController(IUserRepository userRepository, IAuthenticationHelper authenticationHelper)
        {
            _userRepository = userRepository;
            _authenticationHelper = authenticationHelper;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = _userRepository.GetByName(model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!_authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                isAdmin = user.IsAdmin,
                token = _authenticationHelper.GenerateToken(user)
            });
        }
    }
}