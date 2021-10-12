namespace dotnet_graphql_workshop.Domain;

public class SessionSpeaker
    {
        public int SessionId { get; set; }

        public Session? Session { get; set; }

        public int SpeakerId { get; set; }

        public Speaker? Speaker { get; set; }
    }


