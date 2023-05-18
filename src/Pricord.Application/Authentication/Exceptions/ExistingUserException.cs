namespace Pricord.Application.Authentication.Exceptions;

public sealed class ExistingUserException : Exception
{
    public ExistingUserException() : base("User already exists")
    {
    }
}