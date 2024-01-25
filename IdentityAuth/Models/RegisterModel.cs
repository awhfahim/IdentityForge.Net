using Autofac;
using IdentityAuth.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace IdentityAuth.Models
{
    public class RegisterModel
    {
        public class RegistrationModel
        {
            private SignInManager<ApplicationUser> _signInManager;
            private UserManager<ApplicationUser> _userManager;
            private IUserStore<ApplicationUser> _userStore;

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required, DisplayName("First Name")]
            public string FirstName { get; set; }
            [Required, DisplayName("Last Name")]
            public string LastName { get; set; }

            public string? ReturnUrl { get; set; }

            public RegistrationModel()
            {
                
            }
            public RegistrationModel(SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> _userManager, IUserStore<ApplicationUser> _userStore)
            {
                this._signInManager = _signInManager;
                this._userManager = _userManager;
                this._userStore = _userStore;
            }
            internal void Resolve(ILifetimeScope _scope)
            {
                _userManager = _scope.Resolve<UserManager<ApplicationUser>>();
                _signInManager = _scope.Resolve<SignInManager<ApplicationUser>>();
                _userStore = _scope.Resolve<IUserStore<ApplicationUser>>();
            }

            public async Task<(IEnumerable<IdentityError>? errors, string? redirectLocation)> OnPostAsync(string urlPrefix)
            {
                ReturnUrl ??= urlPrefix;

                var user = new ApplicationUser { UserName = Email, Email = Email, FirstName = FirstName, LastName = LastName };

                await _userStore.SetUserNameAsync(user, Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {

                    //var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = userId, code = code, returnUrl = ReturnUrl },
                    //    protocol: Request.Scheme);

                    var callbackUrl = $"{urlPrefix}/Account/ConfirmEmail?userId={user.Id}&code={code}&returnUrl={ReturnUrl}";

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var confirmationPageLink = $"RegisterConfirmation?email={Email}&returnUrl={ReturnUrl}";
                        return (null, confirmationPageLink);
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return (null, ReturnUrl);
                    }
                }
                else
                {
                    return (result.Errors, null);
                }

            }

        }
    }
}
