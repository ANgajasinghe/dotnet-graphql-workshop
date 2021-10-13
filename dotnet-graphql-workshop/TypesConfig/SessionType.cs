using dotnet_graphql_workshop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop.TypesConfig
{
    public class SessionType : ObjectType<Session>
    {
        protected override void Configure(IObjectTypeDescriptor<Session> descriptor)
        {
            descriptor.Description("Sessions are the most good thing to share knowledge");

            descriptor.Authorize();
        }
    }
}
