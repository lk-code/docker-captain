using DockerCaptain.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerCaptain.Data.Repositories;

public class ContainerRepository : IContainerRepository
{
    private readonly DataContext _dataContext;

    public ContainerRepository(DataContext dataContext)
    {
        this._dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }
}
