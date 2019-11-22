using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MediaCalendar.Users
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        UserManager<ApplicationUser> userManager;

        public MyUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
            this.userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("Id", user.Id.ToString()));
            identity.AddClaim(new Claim("FullName", user.FullName));
            identity.AddClaim(new Claim("BankAccount", user.BankAccount));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));

            return identity;
        }

    }
}
