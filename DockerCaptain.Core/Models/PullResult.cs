using System;

namespace DockerCaptain.Core.Models;

public class PullResult
{
    public string Id { get; }
    public string Name { get; }
    public string Tag { get; }
    public string Digest { get; }
    public string? Status { get; }

    public PullResult(string id,
        string name,
        string tag,
        string digest,
        string? status)
    {
        this.Id = id;
        this.Name = name;
        this.Tag = tag;
        this.Digest = digest;
        this.Status = status;
    }
}