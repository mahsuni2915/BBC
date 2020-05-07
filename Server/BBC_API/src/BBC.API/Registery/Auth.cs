﻿using BBC.Core.Configuration.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC.API.Registery
{
    public static class Auth
    {
        public static void UseAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            SymmetricSecurityKey signingKey =
                   new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration[ConfigurationKeys.Jwt + ":Key"].ToString()));
            string Issuer = Configuration[ConfigurationKeys.Jwt + ":Issuer"].ToString();
            string Audience = Configuration[ConfigurationKeys.Jwt + ":Audience"].ToString();

            services
                 .AddAuthentication(o => o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(o =>
                 {
                     //o.RequireHttpsMetadata = false;
                     //o.SaveToken = false;
                     o.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero,

                         ValidIssuer = Issuer,
                         ValidAudience = Audience,
                         IssuerSigningKey = signingKey
                     };
                     o.Events = new JwtBearerEvents
                     {
                         //Token süresi biterse clientin anlaması için Token-Expired keywordü varmı yokmu die bakar
                         //Headerda keyword varsa refresh token 
                         OnAuthenticationFailed = context =>
                         {
                             if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                             {
                                 context.Response.Headers.Add("Token-Expired", "true");
                                 context.Response.StatusCode = 401;
                             }
                             return Task.CompletedTask;
                         }
                     };
                 });

        }

        public static void AuthorizeBuilder(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
