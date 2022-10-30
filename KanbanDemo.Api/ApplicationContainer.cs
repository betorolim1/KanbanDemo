using KanbanDemo.Core.Handlers;
using KanbanDemo.Core.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KanbanDemo.API
{
    public static class ApplicationContainer
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<ILoginHandler, LoginHandler>();
        }
    }
}
