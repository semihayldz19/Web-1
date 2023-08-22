using Course.API.Helper;
using Course.Enums;
using Course.InAndOutModels;
using Course.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _company;
        IOptions<AppSettings> _options;
        private IOptions<AppSettings>? options;

        public CompanyController(ICompany company)
        {
            _company = company;
            _options = options;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MCompany.Form form)
        {

            //var currentUserId = CurrentUser.Get(HttpContext);
            var o = _company.Add(form, null).Result;
            return await Task.FromResult(StatusCode(o.Status, o));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            var currentUserId = CurrentUser.Get(HttpContext);
            var o = _company.Delete(id, currentUserId).Result;
            return await Task.FromResult(StatusCode(o.Status, o));

        }
        [HttpPost]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet([FromForm] MCompany.FilterForm form)
        {

            var currentUserId = CurrentUser.Get(HttpContext);
            var o = _company.MultipleGet(form, currentUserId).Result;
            return await Task.FromResult(StatusCode(o.Status, o));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> SingleGet(string id)
        {

            var currentUserId = CurrentUser.Get(HttpContext);
            var o = _company.SingleGet(id, currentUserId).Result;
            return await Task.FromResult(StatusCode(o.Status, o));

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] MCompany.Form form)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var o = _company.Update(form, currentUserId).Result;
            return await Task.FromResult(StatusCode(o.Status, o));
        }

    }
}




