using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace GREEDY.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly string Secret = Environments.AppConfig.AuthenticationSecret;

        public string GenerateToken(string username, int expireHours)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = now.AddHours(Convert.ToInt32(expireHours)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public Task<bool> ValidateToken(string token)
        {
            return ValidateToken(token, out var username);
        }

        public Task<bool> ValidateToken(string token, out string username)
        {
            username = null;

            try
            {
                var simplePrinciple = GetPrincipal(token);

                if (!(simplePrinciple.Identity is ClaimsIdentity identity)) return Task.FromResult(false);

                if (!identity.IsAuthenticated) return Task.FromResult(false);

                var usernameClaim = identity.FindFirst(ClaimTypes.Name);
                username = usernameClaim?.Value;

                if (string.IsNullOrEmpty(username)) return Task.FromResult(false);
            }
            catch (SecurityTokenValidationException)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        private ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken jwtToken)) return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);

                return principal;
            }
            catch (Exception e)
            {
                //TODO: If we have a log then we definetly need to log the exception
                throw new SecurityTokenValidationException("failed to validate", e);
            }
        }
    }
}