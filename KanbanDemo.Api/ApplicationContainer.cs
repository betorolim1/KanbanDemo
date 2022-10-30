using KanbanDemo.API.Filters;
using KanbanDemo.Core.Domains.Cards.Repository;
using KanbanDemo.Core.Handlers;
using KanbanDemo.Core.Handlers.Interfaces;
using KanbanDemo.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace KanbanDemo.API
{
    public static class ApplicationContainer
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<ILoginHandler, LoginHandler>();
            services.AddScoped<ICardHandler, CardHandler>();
        }
        
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICardRepository, CardRepository>();
        }

        public static void AddFilters(this IServiceCollection services)
        {
            services.AddScoped<LogFilter>();
        }
    }
}
