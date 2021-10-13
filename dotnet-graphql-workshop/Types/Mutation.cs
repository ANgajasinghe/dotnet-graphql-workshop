using dotnet_graphql_workshop.Application.Speakers.Command;
using dotnet_graphql_workshop.Application.Tracks;
using dotnet_graphql_workshop.Domain;
using dotnet_graphql_workshop.Persistence;
using HotChocolate;
using MediatR;

namespace dotnet_graphql_workshop.Types;

// commands
public class Mutation
{

    // mutation AddSpeaker {
    //    addSpeaker(input:{
    //    name: "ABC",
    //    bio: "Saman Kumara from colombo",
    //    webSite: "http://speaker.website"
    //    }) {
    //    speaker {
    //      id,
    //      name
    //},
    //    uid
    //  }
    //}
    public async Task<AddSpeakerPayload> AddSpeakerAsync(AddSpeaker input,[Service] IMediator mediator)
        => await mediator.Send(input);

    public async Task<AddTrackPayload> AddTrackAsync(AddTrack input, [Service] IMediator mediator)
        => await mediator.Send(input);

}

