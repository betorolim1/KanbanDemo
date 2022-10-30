using KanbanDemo.Core.Handlers.Interfaces;
using KanbanDemo.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KanbanDemo.API.Controllers
{
    [Route(Routes.Login)]
    public class LoginController : ControllerBase
    {
        private ILoginHandler _loginHandler;

        public LoginController(ILoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _loginHandler.Login(command);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
