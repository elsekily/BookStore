using BookStoreLibrary.Controllers.Resources;
using BookStoreLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BookStoreLibrary.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [Authorize]
        [HttpPost("api/account/create/user")]
        public IActionResult RegisterUser([FromBody] SaveUserResource userAccount)
        {
            var user = new User()
            {
                Email = userAccount.Email,
                UserName = userAccount.UserName
            };
            userManager.CreateAsync(user, userAccount.Password).Wait();

            var registeredUser = userManager.FindByNameAsync(user.UserName).Result;
            userManager.AddToRoleAsync(registeredUser, "Member").Wait();
            return Ok(new 
            {
                registeredUser.Id,
                registeredUser.Email,
                registeredUser.UserName,
                Roles = userManager.GetRolesAsync(registeredUser).Result,
            });
        }

        [AllowAnonymous]
        [HttpPost("api/account/login")]
        public IActionResult Login([FromBody] SaveUserResource userAccount)
        {
            var userFromDb = userManager.FindByEmailAsync(userAccount.Email).Result;
            var result = signInManager.CheckPasswordSignInAsync(userFromDb, userAccount.Password, false).Result;


            if (result.Succeeded)
            {
                var tokenString = GenerateJSONWebToken(userFromDb);
                return Ok(new
                {
                    userFromDb.Id,
                    userFromDb.Email,
                    userFromDb.UserName,
                    Roles = userManager.GetRolesAsync(userFromDb).Result,
                    tokenString
                });
            }

            return Unauthorized();
        }
        private string GenerateJSONWebToken(User user)
        {
            var claims = new List<Claim>();
            var roles = userManager.GetRolesAsync(user).Result;
            claims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = credentials
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(token));
        }
        /*
        //[Authorize]
        [HttpGet("api/account/intialize")]
        public IActionResult Intialize()
        {/*
            var roles = new List<Role>
            {
                new Role()
                {
                    Name = Policies.Admin
                },
                new Role()
                {
                    Name = Policies.Moderator
                }
            };
            foreach (var role in roles)
            {
                roleManager.CreateAsync(role).Wait();
            }
            var adminUser = new User()
            {
                UserName = "Admin",
                Email = "admin@admin.com"
            };
            userManager.CreateAsync(adminUser, "BsL-12345678900").Wait();
            var admin = userManager.FindByEmailAsync(adminUser.Email).Result;
            userManager.AddToRoleAsync(admin, Policies.Admin).Wait();
            userManager.AddToRoleAsync(admin, Policies.Moderator).Wait();

            return Ok(new
            {
                admin.Email,
                admin.UserName,
                Roles = userManager.GetRolesAsync(admin).Result
            });
        }*/
    }
}
