using Autofac;
using IdentityAuth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityAuth.Models.RegisterModel;

namespace IdentityAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(ILogger<AccountController> _logger, ILifetimeScope _scope) : Controller
    {
        [HttpGet]
        public void Register()
        {
            var model = _scope.Resolve<RegisterModel>();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                var response = await model.OnPostAsync(Url.Content("~/"));

                if (response.errors is not null)
                {
                    foreach (var error in response.errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                    return Ok(model);
            }

            return Ok(model);
        }
    }
}
