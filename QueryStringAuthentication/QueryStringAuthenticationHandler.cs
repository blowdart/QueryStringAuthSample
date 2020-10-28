// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace QueryStringAuthentication
{
    public class QueryStringAuthenticationHandler : AuthenticationHandler<QueryStringAuthenticationOptions>
    {
        public QueryStringAuthenticationHandler(
            IOptionsMonitor<QueryStringAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.QueryString.HasValue)
            {
                // No query string, no point in continuing.
                return AuthenticateResult.NoResult();
            }

            if (!Request.Query.Keys.Contains(Options.UserNameParameter))
            {
                // No user name, so we can't make a minimum viable user.
                return AuthenticateResult.NoResult();
            }

            var testIdentity = new ClaimsIdentity(Scheme.Name);
            testIdentity.AddClaim(new Claim(ClaimTypes.Name, Request.Query[Options.UserNameParameter].First(), ClaimValueTypes.String, ClaimsIssuer));

            if (Request.Query.Keys.Contains(Options.RolesParameter))
            {
                foreach (string role in Request.Query[Options.RolesParameter].First().Split(","))
                {
                    testIdentity.AddClaim(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, ClaimsIssuer));
                }
            }

            var testPrincipal = new ClaimsPrincipal(testIdentity);
            var ticket = new AuthenticationTicket(testPrincipal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
