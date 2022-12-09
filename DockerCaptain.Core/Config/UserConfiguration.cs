using System;
using System.Text.Json.Serialization;

namespace DockerCaptain.Core.Config;

public class UserConfiguration
{
    [JsonPropertyName("docker")]
    public string? Docker { get; set; }
}