using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityAuth.Membership
{
    public class ApplicationUserManager(IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
        : UserManager<ApplicationUser>(store, optionsAccessor, passwordHasher, userValidators, 
            passwordValidators, keyNormalizer, errors, services, logger)
    {
    }
}
