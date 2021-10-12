using code365University.Persistence;
using GraphQL.Server.Ui.Voyager;
using code365University.Types;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppConnection");

builder.Services
    .AddDbContext<SchoolContext>(
        (s, o) => o
            .UseSqlServer(connectionString)
            .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()))
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .ConfigureResolverCompiler(c => c.AddService<SchoolContext>())
    .ModifyOptions(o => o.DefaultResolverStrategy = ExecutionStrategy.Serial)
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .EnsureDataIsSeeded()
    .InitializeOnStartup();


var app = builder.Build();

app.MapGraphQL();
app.MapGet("/", () => "Hello World!");


app.UseGraphQLPlayground("/graphql-playground");

app.UseGraphQLVoyager("/graphql-voyager");

app.Run();
