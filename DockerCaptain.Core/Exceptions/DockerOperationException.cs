using System;
namespace DockerCaptain.Core.Exceptions;

public class DockerOperationException : Exception
{
    public DockerOperationException()
    {
    }
    public DockerOperationException(string message) : base(message)
    {
    }
    public DockerOperationException(string message, Exception inner) : base(message, inner)
    {
    }
}