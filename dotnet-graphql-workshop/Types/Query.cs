using dotnet_graphql_workshop.Domain;
using dotnet_graphql_workshop.Persistence;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop
{
    public class Query
    {
        public IQueryable<Speaker> GetSpeakers([Service] AppDbContext context)
            => context.Speakers;
    }
}
