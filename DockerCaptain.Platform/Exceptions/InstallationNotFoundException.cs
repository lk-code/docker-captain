using System;
namespace DockerCaptain.Core.Exceptions;

public class InstallationNotFoundException : Exception
{
    public InstallationNotFoundException()
    {
    }
    public InstallationNotFoundException(string message) : base(message)
    {
    }
    public InstallationNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}