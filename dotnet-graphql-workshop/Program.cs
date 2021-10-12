using dotnet_graphql_workshop;
using dotnet_graphql_workshop.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddGraphQLService();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseRouting();
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");
app.UseGraphQLPlayground("/graphql-playground");
app.UseGraphQLVoyager("/graphql-voyager");

app.Run();
