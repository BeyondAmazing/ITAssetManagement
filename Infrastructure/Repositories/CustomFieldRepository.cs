using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CustomFieldRepository : GenericRepository<CustomField>, ICustomFieldRepository
{
    public CustomFieldRepository(DbCtx context) : base(context)
    {

    }
}
