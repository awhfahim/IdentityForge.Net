using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IdentityAuth.Membership
{
    public class ApplicationUserRole
        : IdentityUserRole<Guid>
    {
       
    }
}
