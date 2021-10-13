using dotnet_graphql_workshop.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop.Application.Sessions
{
    public record UpdateSession(int id,string Title, string? Abstract, DateTimeOffset? StartTime, DateTimeOffset? EndTime, int? TrackId) : IRequest<AddSessionPayload>;



    public class UpdateSessionHandler : IRequestHandler<UpdateSession, AddSessionPayload>
    {
        private readonly AppDbContext _appDbContext;

        public UpdateSessionHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<AddSessionPayload> Handle(UpdateSession request, CancellationToken cancellationToken)
        {

            // get session

            var session =  await _appDbContext.Sessions.Where(x => x.Id == request.id).FirstOrDefaultAsync() ?? throw new ArgumentException("Invalid Id");

            // patch

            session.Title = request.Title;
            session.Abstract = request.Abstract;
            session.StartTime = request.StartTime;  
            session.EndTime = request.EndTime;  
            session.TrackId = request.TrackId;


            _appDbContext.Sessions.Update(session);
            await _appDbContext.SaveChangesAsync();

            return new AddSessionPayload(session);

        }
    }
}
