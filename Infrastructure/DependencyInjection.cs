using Application.ToDo;
using Domain.Interface.ToDo;
using Infrastructure.ToDo;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ToDo
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connection)
        {
            services.AddScoped<IToDoRepository, ToDoRepository>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                                        ConnectionMultiplexer.Connect(connection));
            return services;
        }
    }
}
