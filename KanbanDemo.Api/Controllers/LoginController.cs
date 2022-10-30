using KanbanDemo.Core.Handlers.Interfaces;
using KanbanDemo.Core.Requests;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login([FromBody] LoginCommand command)
        {
            var token = _loginHandler.Login(command);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
