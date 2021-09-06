// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace GestionarePacienti.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {

               // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "mvc",
                    ClientName="MVC Client",
                    ClientSecrets = { new Secret("mvcSecret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    AllowedCorsOrigins={"https://localhost:44301"},

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:44301/signin-oidc" },
                    
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:44301/signout-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile" }
                }
            };
    }
}