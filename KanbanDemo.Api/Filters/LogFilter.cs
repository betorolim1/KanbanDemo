using KanbanDemo.Core.Domains.Cards.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanDemo.API.Filters
{
    public class LogFilter : IActionFilter
    {
        private const string UPDATE_CARD = "UpdateCard";
        private const string ACTION_ALTERAR = "Alterar";

        private const string DELETE_CARD = "DeleteCard";
        private const string ACTION_REMOVER = "Remover";

        //private ICardRepository _cardRepository;

        //public LogFilter(ICardRepository cardRepository)
        //{
        //    _cardRepository = cardRepository;
        //}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var path = context.HttpContext.Request.Path.Value;

            var actionName = context.ActionDescriptor.RouteValues["controller"] + "/" + context.ActionDescriptor.RouteValues["action"];

            //var card = _cardRepository.GetCardById()

            // Console.WriteLine("");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
