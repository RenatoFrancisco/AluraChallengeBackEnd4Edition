namespace AluraChallengeBackEnd.Core.DomainObjects;

public class DomainException : Exception
{
    public DomainException() {}
        
    public DomainException(string message) : base(message) {}

    public DomainException(string message, Exception inner) : base(message, inner) {}

    public DomainException(SerializationInfo info, StreamingContext context) : base(info, context) {}
}