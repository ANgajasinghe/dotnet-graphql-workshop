using dotnet_graphql_workshop.Application.Speakers.Query;
using dotnet_graphql_workshop.Domain;
using dotnet_graphql_workshop.Persistence;
using HotChocolate;
using JWT.Algorithms;
using JWT.Builder;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        [UsePaging(MaxPageSize = 50)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Session> GetSessions([Service] AppDbContext context)
            => context.Sessions;


        [UsePaging(MaxPageSize = 50)]
        [UseProjection]
        [UseFiltering]
        public IQueryable<Track> GetTracks([Service] AppDbContext context)
            => context.Tracks;



        // sample
        public async Task<Speaker> GetSpeakerAsync(int id, [Service] IMediator mediator)
            => await mediator.Send(new GetSpeackerById(id));


        public string GetToken(string email,string role) 
        {
            var payload = new Dictionary<string, object>
            {
                { JwtRegisteredClaimNames.Sub, email },
                { "name", email },
                { "email", email },
            };

            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

            var token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                      .WithSecret(secret)
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaims(payload)
                      .AddClaim("role", role)
                      .Encode();

            return token;
        }

    }
}
