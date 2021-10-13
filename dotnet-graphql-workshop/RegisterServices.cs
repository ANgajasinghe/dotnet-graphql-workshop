using dotnet_graphql_workshop.Application.Sessions;
using dotnet_graphql_workshop.Persistence;
using dotnet_graphql_workshop.Types;
using dotnet_graphql_workshop.TypesConfig;
using HotChocolate;
using HotChocolate.Execution;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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



            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk")),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };




            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(jwt =>
               {
                   jwt.SaveToken = true;
                   jwt.TokenValidationParameters = tokenValidationParams;
               });

            services.AddAuthorization();


            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddGraphQLService(this IServiceCollection services)
        {
            // get all type from assembly
            var typeConfigs = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(x => x.Namespace == typeof(SessionType).Namespace).ToArray();

            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .ConfigureResolverCompiler(c => c.AddService<AppDbContext>())
                .ModifyOptions(o => o.DefaultResolverStrategy = ExecutionStrategy.Serial)
                .AddProjections()
                .AddAuthorization()
                .AddFiltering()
                .AddTypes(typeConfigs)
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
