using KanbanDemo.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanDemo.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _jwtKey;

        // Este Middleware faz uma validação parecida com a validação da Microsoft (configurada na Startup),
        // mas este foi desenvolvido para contemplar o requisito do desafio.
        public JwtMiddleware(RequestDelegate next, string jwtKey)
        {
            _next = next;
            _jwtKey = jwtKey;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Value.Contains(Routes.Login))
            {
                var isValid = IsValidToken(context);

                if (!isValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next(context);
        }

        private bool IsValidToken(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token is null)
                    return false;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = TokenConfigurations.Issuer,
                    ValidAudience = TokenConfigurations.Audience,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
