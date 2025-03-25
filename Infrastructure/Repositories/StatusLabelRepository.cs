using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class StatusLabelRepository : GenericRepository<StatusLabel>, IStatusLabelRepository
{
    public StatusLabelRepository(DbCtx context) : base(context)
    {

    }
}
