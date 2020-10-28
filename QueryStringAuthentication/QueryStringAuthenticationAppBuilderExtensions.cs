// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Authentication;
using QueryStringAuthentication;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods to add the test query string authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class QueryStringAuthenticationAppBuilderExtensions
    {
        public static AuthenticationBuilder AddQueryString(this AuthenticationBuilder builder)
            => builder.AddQueryString(QueryStringAuthenticationDefaults.AuthenticationScheme);

        public static AuthenticationBuilder AddQueryString(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddQueryString(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddQueryString(this AuthenticationBuilder builder, Action<QueryStringAuthenticationOptions> configureOptions)
            => builder.AddQueryString(QueryStringAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddQueryString(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            Action<QueryStringAuthenticationOptions> configureOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddScheme<QueryStringAuthenticationOptions, QueryStringAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }
}
