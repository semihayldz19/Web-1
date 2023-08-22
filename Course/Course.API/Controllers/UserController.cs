using Course.API.Controllers;
using Course.API.Helper;
using Course.Enums;
using Course.InAndOutModels;
using Course.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Course.API.Controllers;
using System.Security.Claims;
using System.Text;

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        IOptions<AppSettings> _options;

        public object JwtRegisteredClaimNames { get; private set; }

        public UserController(IOptions<AppSettings> options, IUser user)
        {
            _user = user;
            _options = options;
        }

        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromForm] MUser.LoginForm form)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var mr = _user.Login(form, currentUserId).Result;
            if (mr.Status == 200)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId,mr.Item.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken
                    (
                        issuer: _options.Value.Jwt.Issuer,
                        audience: _options.Value.Jwt.Audience,
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(30),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.Key)), SecurityAlgorithms.HmacSha256)

                    );
                string tokenstr = new JwtSecurityTokenHandler().WriteToken(token);
                mr.Item.Token = tokenstr;
            }
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MUser.Form form)
        {
            //var currentUserId = CurrentUser.Get(HttpContext);
            var o = _user.Add(form, null).Result;
            return await Task.FromResult(StatusCode(o.Status, o));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);

            var o = _user.Delete(id, currentUserId).Result;
            return await Task.FromResult(StatusCode(o.Status, o));

        }

        [HttpPost]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet([FromForm] MUser.FilterForm form)
        {
            var currentUserId = CurrentUser.Get(HttpContext);

            var o = _user.MultipleGet(form, currentUserId).Result;
            return await Task.FromResult(StatusCode(o.Status, o));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SingleGet(string id)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var o = _user.SingleGet(id, currentUserId).Result;
            return await Task.FromResult(StatusCode(o.Status, o));

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] MUser.Form form)
        {
            var currentUserId = CurrentUser.Get(HttpContext);

            var o = _user.Update(form, currentUserId).Result;
            return await Task.FromResult(StatusCode(o.Status, o));
        }
    }
}

