using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityAuth.Membership
{
    public class ApplicationRoleManager(IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger)
        : RoleManager<ApplicationRole>(store, roleValidators, keyNormalizer, errors, logger)
    {

    }
}
