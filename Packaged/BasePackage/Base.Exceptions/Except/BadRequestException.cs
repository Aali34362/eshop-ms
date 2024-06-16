namespace Base.Exceptions.Except;

public class BadRequestException : Exception
{
    public BadRequestException()
        : base()
    {
    }    
    public BadRequestException(string message) 
        : base(message)
    {
        
    }
    public string? Details { get; }
    public BadRequestException(string message, string details)
        : base(message)
    {
        Details = details;
    }
    public BadRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
