using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerCaptain.Data.Models;

public class Image
{
    [Key]
    [Required]
    public string DockerId { get; set; }
    [Required]
    public string Name { get; set; }

    public Image(string dockerId,
        string name)
    {
        DockerId = dockerId;
        Name = name;
    }
}