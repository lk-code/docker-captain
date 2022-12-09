using System;
using Microsoft.Extensions.Logging;

namespace DockerCaptain.Logging;

public sealed class FileLoggerConfiguration
{
    public int EventId { get; set; }
    public string? LogPath { get; set; }
    public string? LogFileDateTemplate { get; set; }
}