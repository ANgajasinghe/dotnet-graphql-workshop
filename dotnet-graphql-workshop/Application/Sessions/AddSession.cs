using dotnet_graphql_workshop.Domain;
using dotnet_graphql_workshop.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop.Application.Sessions
{
    public record AddSession(string Title, string? Abstract, DateTimeOffset? StartTime, DateTimeOffset? EndTime, int? TrackId) : IRequest<AddSessionPayload>;


    public class AddSessionPayload
    {
        public AddSessionPayload(Session session)
        {
            Session = session;
        }

        public Session Session { get; set; }
    }

    public class AddSessionHandler : IRequestHandler<AddSession, AddSessionPayload>
    {
        private readonly AppDbContext _appDbContext;

        public AddSessionHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<AddSessionPayload> Handle(AddSession request, CancellationToken cancellationToken)
        {
            var session = new Session { Title = request.Title, Abstract = request.Abstract, StartTime = request.StartTime, EndTime = request.EndTime, TrackId = request.TrackId };
            await _appDbContext.Sessions.AddAsync(session);
            await _appDbContext.SaveChangesAsync();

            return new AddSessionPayload(session);

        }
    }

}
