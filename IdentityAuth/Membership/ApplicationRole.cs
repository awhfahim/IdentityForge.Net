using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityAuth.Membership
{
    public class ApplicationRole(string roleName) : IdentityRole<Guid>(roleName)
    {
        public ApplicationRole()
            : this("")
        {
        }
    }
}
