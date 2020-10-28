// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authentication;

namespace QueryStringAuthentication
{
    public class QueryStringAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string UserNameParameter { get; set; } = "UserName";
        public string RolesParameter { get; set; } = "UserRoles";
    }
}
