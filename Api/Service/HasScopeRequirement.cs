using Microsoft.AspNetCore.Authorization;

namespace Api.Service
{
    public class HasScopeRequirement:IAuthorizationRequirement
    {
        public HasScopeRequirement(string scope ,string issuer)
        {
            Scope=scope ?? throw new ArgumentException(nameof(scope));
            Issuer=issuer ?? throw new ArgumentException(nameof(issuer));
        }

        public string Issuer { get; set; }

        public string Scope { get; set; }

    }
}
