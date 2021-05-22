using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MineSweeper.Domain.Interfaces.Facades;
using MineSweeper.Domain.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.CrossCutting.Auth.Facades
{
    public class AuthFacade : IAuthFacade
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthFacade(IConfiguration configuration,
                          IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken(Guid userId, string userName)
        {
            try
            {
                TokenConfigurationsSettings tokenSettings = _configuration.GetSection(nameof(TokenConfigurationsSettings))?.Get<TokenConfigurationsSettings>();

                if (tokenSettings == null)
                    throw new Exception("There's no TokenSettings defined!");

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

                SecurityTokenDescriptor _tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = GetClaimsForUser(userId, userName),
                    Expires = DateTime.UtcNow.AddMinutes(tokenSettings.ExpiresIn),
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                                SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken generatedToken = tokenHandler.CreateToken(_tokenDescriptor);

                return tokenHandler.WriteToken(generatedToken);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Guid> GetLoggedUserId()
        {
            IEnumerable<Claim> claims = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).Claims;
            string userId = claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException();

            return Guid.Parse(userId);
        }

        private ClaimsIdentity GetClaimsForUser(Guid userId, string userName)
        {
            ClaimsIdentity _claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.PrimarySid, userId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, string.Empty),
            }, "Bearer");

            return _claims;
        }
    }
}
