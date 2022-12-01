namespace DockerCaptain.PlatformCore.Exceptions;

public class NotSupportedPlatformException : Exception
{
    public NotSupportedPlatformException() : base()
    {

    }
    public NotSupportedPlatformException(string message) : base(message)
    {

    }
    public NotSupportedPlatformException(string message, Exception inner) : base(message, inner)
    {

    }
}
