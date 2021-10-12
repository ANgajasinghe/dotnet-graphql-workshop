using dotnet_graphql_workshop.Application.Speakers.Query;
using dotnet_graphql_workshop.Domain;
using dotnet_graphql_workshop.Persistence;
using HotChocolate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop
{
    public class Query
    {

        //query GetSpeakerWithWhere
        // {
        // speakers(where: {
        //    id : {
        //      eq : 1
        //    }
        //}) {
        //    name
        //    bio
        //    sessionSpeakers {
        //        sessionId
        //    }
        //  }
        //}
        [UsePaging(MaxPageSize = 50)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Speaker> GetSpeakers([Service] AppDbContext context)
            => context.Speakers;

        public async Task<Speaker> GetSpeakerAsync(int id, [Service] IMediator mediator)
            => await mediator.Send(new GetSpeackerById(id));

    }
}
