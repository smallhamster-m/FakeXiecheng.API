using FakeXiecheng.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FakeXiecheng.API.Models;
using FakeXiecheng.API.Services;

namespace FakeXiecheng.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITouristRouteRepository _touristRouteRepository;
        public AuthenticateController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITouristRouteRepository touristRouteRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _touristRouteRepository = touristRouteRepository;
        }
        [AllowAnonymous] 
        [HttpPost("login")] 
        public async Task<IActionResult >Login([FromBody] LoginDto loginDto)
        {
            //1.验证用户名和密码
          var res = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (!res.Succeeded){
                return BadRequest();
            }
            var user = await _userManager.FindByNameAsync(loginDto.Email);

            //创建JWT
            //签名算法HS256
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;
            //payload
            var claims = new List<Claim> { 
                //sub
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                //new Claim(ClaimTypes.Role,"Admin")//添加网站管理员角色
            };
            //获取用户的角色列表
            var roleNames =await _userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                var roleClaim = new Claim(ClaimTypes.Role, roleName);
                claims.Add(roleClaim);
            }
            //signiture
            //把私钥转换成编码
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);
            //加密
            var signingKey = new SymmetricSecurityKey(secretByte);
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm);
            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials
             );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            //成功返回200ok+token
            return Ok(tokenStr);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) {
            var user = new ApplicationUser()
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };
            var res = await _userManager.CreateAsync(user, registerDto.Password);
            if (!res.Succeeded) {
                return BadRequest();
            }

            //初始化用户购物车
            var shoppingCart = new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
            };
            await _touristRouteRepository.CreateShoppingCart(shoppingCart);
            await _touristRouteRepository.SaveAsync();

            return Ok();
        }
    }
}
