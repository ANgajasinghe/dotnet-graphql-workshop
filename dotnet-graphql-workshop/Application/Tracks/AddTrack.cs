using dotnet_graphql_workshop.Domain;
using dotnet_graphql_workshop.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop.Application.Tracks
{
    public record AddTrack(string name) : IRequest<AddTrackPayload>;

    public class AddTrackPayload
    {
        public AddTrackPayload(Track track)
        {
            Track = track;
        }

        public Track Track { get; set; }
    }

    public class AddTrackHandler : IRequestHandler<AddTrack, AddTrackPayload>
    {
        private readonly AppDbContext _appDbContext;

        public AddTrackHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<AddTrackPayload> Handle(AddTrack request, CancellationToken cancellationToken)
        {
            var track = new Track { Name = request.name };
            await _appDbContext.Tracks.AddAsync(track);
            await _appDbContext.SaveChangesAsync();

            return new AddTrackPayload(track);

        }
    }

}
