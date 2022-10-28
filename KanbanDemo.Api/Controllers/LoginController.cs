using KanbanDemo.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KanbanDemo.API.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public async Task<IActionResult> Login(LoginCommand command)
        {

        }
    }
}
