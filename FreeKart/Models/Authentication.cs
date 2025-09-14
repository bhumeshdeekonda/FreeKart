using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FreeKart.Models
{
    public class Authentication
    {
        public static string GenerateJwtToken(string username, List<string> roles)
        {
            var claims = new List<Claim>
            {
                // Only one sub claim, representing the subject (username)
                new Claim(JwtRegisteredClaimNames.Sub, username),

                // Optional: A unique token identifier (jti)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(ClaimTypes.NameIdentifier, username)
            };

            // Add roles to claims properly
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["config:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(ConfigurationManager.AppSettings["config:Expires"]));

            var token = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSettings["config:JwtIssuer"],
                audience: ConfigurationManager.AppSettings["config:JwtAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}












//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Web;

//namespace FreeKart.Models
//{
//    public class Authentication
//    {

//        public static string GenerateJwtToken(string username, List<string> roles)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(JwtRegisteredClaimNames.Sub,username),
//                new Claim(JwtRegisteredClaimNames.Sub,Guid.NewGuid().ToString()),
//                new Claim(ClaimTypes.NameIdentifier,username)
//            };

//            roles.ForEach(role =>
//            {
//                new Claim(ClaimTypes.Role, role);
//                });

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"])));
//            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
//            var expires = DateTime.Now.AddDays(Convert.ToDouble(Convert.ToString(ConfigurationManager.AppSettings["config:Expires"])));

//            var token = new JwtSecurityToken(
//                issuer: Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
//                audience: Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
//                claims: claims,
//                expires: expires,
//                signingCredentials: creds
//                );

//            return new JwtSecurityTokenHandler().WriteToken(token) ;




//        }
//    }
//}