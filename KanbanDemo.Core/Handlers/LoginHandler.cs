using KanbanDemo.Core.Handlers.Interfaces;
using KanbanDemo.Core.Infrastructure;
using KanbanDemo.Core.Requests;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KanbanDemo.Core.Handlers
{
    public class LoginHandler : ILoginHandler
    {
        private Credentials _credentials { get; set; }
        private SigningConfigurations _signingConfigurations;

        public LoginHandler(Credentials credentials, SigningConfigurations signingConfigurations)
        {
            _credentials = credentials;
            _signingConfigurations = signingConfigurations;
        }

        public string Login(LoginCommand command)
        {
            if (command.Login != _credentials.Login || command.Password != _credentials.Password)
                return null;

            var token = GenerateToken();

            return token;
        }

        private string GenerateToken()
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
                }
            );

            var expires = DateTime.UtcNow.AddHours(1);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenConfigurations.Issuer,
                Audience = TokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = DateTime.Now,
                Expires = expires
            });
            var token = handler.WriteToken(securityToken);

            return token;
        }
    }
}
