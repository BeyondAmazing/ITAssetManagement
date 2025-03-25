using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class DepreciationRepository : GenericRepository<Depreciation>, IDepreciationRepository
{
    public DepreciationRepository(DbCtx context) : base(context)
    {

    }
}
