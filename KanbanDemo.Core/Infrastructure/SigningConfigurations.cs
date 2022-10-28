using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KanbanDemo.Core.Infrastructure
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }


        public SigningConfigurations(string jwtKey)
        {
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            SecurityAlgorithms.HmacSha256);
        }
    }
}
