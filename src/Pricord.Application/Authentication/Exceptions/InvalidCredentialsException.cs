namespace Pricord.Application.Authentication.Exceptions;

public sealed class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Invalid username or password")
    {
    }
}