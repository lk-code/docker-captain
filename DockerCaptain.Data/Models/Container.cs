using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DockerCaptain.Data.Models;

public class Container
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string DockerId { get; set; }
    [Required]
    public string Name { get; set; }

    public Container(string dockerId,
        string name)
    {
        DockerId = dockerId;
        Name = name;
    }
}