using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class ApplicationUserClaimPrincipleFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimPrincipleFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManagers, IOptions<IdentityOptions> options) : base(userManager, roleManagers, options)
        {
        }

        public override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            return base.CreateAsync(user);
        }

        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("UserStatus", user.IsActive.ToString()));
            identity.AddClaim(new Claim("LicenceStatus", user.Doctor.IsLicenceVerified.ToString()));
            identity.AddClaim(new Claim("UserId", user.Id));
            return identity;
        }
    }
}