using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KanbanDemo.API.Controllers
{
    [Route(Routes.Cards)]
    [Authorize]
    public class CardController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            return Ok();
        }
    }
}
