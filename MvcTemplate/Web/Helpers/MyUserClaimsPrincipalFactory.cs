using Domain.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Repository.IRepositories;

namespace Web.Helpers
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public MyUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("AboId", user.Abonnement_ID.ToString()));
            identity.AddClaim(new Claim("logo", user.Logo.ToString()));
            identity.AddClaim(new Claim("posId", user.PositionVente_ID.ToString()));
            identity.AddClaim(new Claim("userId", user.Id));
            return identity;

        }
        
        


    }
}
