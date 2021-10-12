using dotnet_graphql_workshop.Domain;
using dotnet_graphql_workshop.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop.Application.Speakers.Query
{
    public record GetSpeackerById(int Id) : IRequest<Speaker>;

    public class GetSpeackerByIdHandler : IRequestHandler<GetSpeackerById, Speaker>
    {
        private readonly AppDbContext _appDbContext;

        public GetSpeackerByIdHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Speaker> Handle(GetSpeackerById request, CancellationToken cancellationToken)
        {
           return await _appDbContext.Speakers.FirstOrDefaultAsync(x => x.Id == request.Id) 
                ?? throw new ArgumentNullException(nameof(request.Id),"Invalid id");
        }
    }
}
