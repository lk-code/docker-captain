using System;
using Microsoft.Extensions.Logging;

namespace DockerCaptain.Logging;

internal static class LogEvents
{
    // services provider
    internal static EventId ServicesAddPlatform = new EventId(10001, "ServicesAddPlatform");
    internal static EventId ServicesAddCore = new EventId(10002, "ServicesAddCore");
    internal static EventId ServicesAddDatabase = new EventId(10003, "ServicesAddDatabase");

    // commands
    internal static EventId CommandInfoDisplayInfo = new(20001, "CommandInfoDisplayInfo");
    internal static EventId CommandImagesRegister = new(20002, "CommandImagesRegister");
}