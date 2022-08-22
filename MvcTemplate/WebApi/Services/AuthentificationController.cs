using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IAuthentificationService authentificationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;
      //  private readonly IFournisseurService fournisseurService;

        public AuthentificationController(IOptions<AppSettings> appSettings, IAuthentificationService authentificationService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository, IFournisseurService fournisseurService = null)
        {
            _appSettings = appSettings.Value;
            this.authentificationService = authentificationService;
            _userManager = userManager;
            this.authentificationRepository = authentificationRepository;
          //  this.fournisseurService = fournisseurService;
        }

        // GET: api/authentification
        [HttpGet]
        [Authorize]
        public JsonResult Get()
        {
            string message = "Bonjour dans le service d'authentification";

            return new JsonResult(message);
        }

        // POST: api/authentification/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await this.authentificationService.Register(registerModel));
        }
        // POST: api/authentification/EditProfile
        [HttpPost]
        [Route("editProfile")]
        public async Task<IActionResult> EditProfile([FromBody] EditModelApi editModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await this.authentificationService.UpdateUserAsyncApi(editModel.userId, editModel.oldMdp, editModel.newMdp));
        }

        //POST: api/authentification/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await this.authentificationService.LoginMobile(loginModel);

            if (user != null && user.Message == 1)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier , user.UserName),
                    }),

                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                var userRole = await this.authentificationRepository.getRoleUserById((user.Id).ToString());
                user.Token = tokenString;
                user.Role = userRole;
                return Ok(user);
            }
            return Unauthorized();
        }
     
    }
}
