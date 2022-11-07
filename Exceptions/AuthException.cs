namespace TasksWithRepositoryPattern.Exceptions;

public class AuthException : Exception
{
    public override string Message { get; }
    public List<string> Errors;
    public AuthException(string message, List<string> errros)
    {
        Message = message;
        Errors = errros;
    }
}