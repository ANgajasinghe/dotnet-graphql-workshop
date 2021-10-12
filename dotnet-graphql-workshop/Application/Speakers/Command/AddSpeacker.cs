using dotnet_graphql_workshop.Domain;
using dotnet_graphql_workshop.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop.Application.Speakers.Command
{

    // input
    public record AddSpeaker (string Name, string? Bio, string? WebSite) : IRequest<AddSpeakerPayload>;


    // implementation
    public class AddSpeackerHandler : IRequestHandler<AddSpeaker, AddSpeakerPayload>
    {
        private readonly AppDbContext _appDbContext;

        public AddSpeackerHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<AddSpeakerPayload> Handle(AddSpeaker request, CancellationToken cancellationToken)
        {
            var speaker = new Speaker
            {
                Name = request.Name,
                Bio = request.Bio,
                WebSite = request.WebSite
            };

            _appDbContext.Speakers.Add(speaker);
            await _appDbContext.SaveChangesAsync();

            return new AddSpeakerPayload(speaker);
        }
    }

    // payload
    public class AddSpeakerPayload
    {
        public AddSpeakerPayload(Speaker speaker)
        {
            Speaker = speaker;
            Uid = Guid.NewGuid().ToString();   
        }

        public Speaker Speaker { get; }
        public string Uid { get; }
    }
}
