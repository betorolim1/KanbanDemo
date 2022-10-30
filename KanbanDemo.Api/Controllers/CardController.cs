using KanbanDemo.API.Filters;
using KanbanDemo.Core.Commands;
using KanbanDemo.Core.Handlers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KanbanDemo.API.Controllers
{
    [Route(Routes.Cards)]
    [Authorize]
    public class CardController : ControllerBase
    {
        private ICardHandler _cardHandler;

        public CardController(ICardHandler cardHandler)
        {
            _cardHandler = cardHandler;
        }

        [HttpPost]
        public async Task<IActionResult> InsertCardAsync([FromBody] InsertCardCommand command)
        {
            if (command is null)
                return BadRequest("Command não pode ser nula");

            var result = await _cardHandler.InsertCardAsync(command);

            if (!_cardHandler.IsValid)
                return BadRequest(_cardHandler.Notifications);

            return Ok(result);
        }

        [ServiceFilter(typeof(LogFilter))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCardAsync(Guid id, [FromBody] UpdateCardCommand command)
        {
            if (command is null)
                return BadRequest("Command não pode ser nula");

            command.Id = id;

            var result = await _cardHandler.UpdateCardAsync(command);

            if (!_cardHandler.IsValid)
                return BadRequest(_cardHandler.Notifications);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [ServiceFilter(typeof(LogFilter))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardAsync(Guid id)
        {
            var result = await _cardHandler.DeleteCardAsync(id);

            if (!_cardHandler.IsValid)
                return BadRequest(_cardHandler.Notifications);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCardsAsync()
        {
            var result = await _cardHandler.GetCards();

            return Ok(result);
        }
    }
}
