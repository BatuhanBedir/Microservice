// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {                       //bu resource'nın izni       bu
                 new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
                 new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
                 new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
                 new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
                 new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
                 new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       //kullanıcı hakkında hangi bilgilere clientten erişilebilir işlemi
                       new IdentityResources.Email(),//hazır claim
                       new IdentityResources.OpenId(), //jwt almak isteniliyorsa token içerisinde sub(subject) keyword'ün jwt payload'ındaki sub keywordünün dolu olması gerekmektedir.
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name="roles", DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims = new[]{"role"} }//hangi claimle maplenecek.

                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Catalog API için full erişim"),
                new ApiScope("photo_stock_fullpermission","Photo Stock API için full erişim"),
                new ApiScope("basket_fullpermission","Basket API için full erişim"),
                new ApiScope("discount_fullpermission","Discount API için full erişim"),
                new ApiScope("order_fullpermission","Order API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials, //refresh token yok
                    AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true, //OfflineAccess 'ı kullanabilmek icin izin
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {"basket_fullpermission", "discount_fullpermission", "order_fullpermission", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.LocalApi.ScopeName, IdentityServerConstants.StandardScopes.OfflineAccess,"roles"},//refresh. kullanıcı offline olduğunda bile token-kullanıcı adına refresh tokenla yeni token alabilme.
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds, //refresh token ömrü- Token alırken refresh token de alacak.
                    RefreshTokenUsage = TokenUsage.ReUse //tekrar kullanılabilir.
                }
            };
    }
}