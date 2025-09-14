using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FreeKart.Middleware
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var tokenCookie = httpContext.Request.Cookies["jwt_token"];
            if (tokenCookie == null || string.IsNullOrEmpty(tokenCookie.Value))
            {
                return false;
            }

            var token = tokenCookie.Value;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["config:JwtKey"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = ConfigurationManager.AppSettings["config:JwtIssuer"],
                    ValidAudience = ConfigurationManager.AppSettings["config:JwtAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // no clock skew
                }, out SecurityToken validatedToken);

                // Optionally, you can set HttpContext.User here with ClaimsPrincipal
                var jwtToken = (JwtSecurityToken)validatedToken;
                var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                httpContext.User = new ClaimsPrincipal(identity);

                return true;
            }
            catch (Exception)
            {
                // Token validation failed
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirect to login page if unauthorized
            filterContext.Result = new RedirectResult("/auth/Login");
        }
    }
}
