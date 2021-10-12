using dotnet_graphql_workshop.Persistence;
using dotnet_graphql_workshop.Types;
using HotChocolate;
using HotChocolate.Execution;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace dotnet_graphql_workshop
{
    public static class RegisterServices
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("AppConnection");

            services.AddDbContext<AppDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddGraphQLService(this IServiceCollection services) 
        {
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .ConfigureResolverCompiler(c => c.AddService<AppDbContext>())
                .ModifyOptions(o => o.DefaultResolverStrategy = ExecutionStrategy.Serial)
                .AddProjections()
                .AddFiltering()
                .AddErrorFilter(error => 
                {

                    if (error.Exception != null) 
                    {
                      error = error.WithMessage(error.Exception.Message);
                    }

                    return error;
                })
                .ModifyRequestOptions(opt =>
                {
                    // opt.IncludeExceptionDetails = false;
                   
                });

            return services;
        }

    }
}
