using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_graphql_workshop.Domain;

public class Attendee
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(200)]
    public string? LastName { get; set; }

    [Required]
    [StringLength(200)]
    public string? UserName { get; set; }

    [StringLength(256)]
    public string? EmailAddress { get; set; }

    public ICollection<SessionAttendee> SessionsAttendees { get; set; } =
        new List<SessionAttendee>();
}


