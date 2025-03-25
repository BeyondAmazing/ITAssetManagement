using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomFieldSetRepository : GenericRepository<CustomFieldSet>, ICustomFieldSetRepository
{
    public CustomFieldSetRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<CustomFieldSet?> GetByIdAsync(Guid id)
    {
        return await _context.CustomFieldSets
            .Include(cf => cf.CustomFields)
            .FirstOrDefaultAsync(cf => cf.Id == id);
    }

    public override async Task<IEnumerable<CustomFieldSet>> GetAllAsync()
    {
        return await _context.CustomFieldSets
            .Include(cf => cf.CustomFields)
            .ToListAsync();
    }

    public async Task AddFieldToSetAsync(Guid fieldsetId, Guid fieldId)
    {
        var customFieldset = await GetByIdAsync(fieldsetId);
        var customField = await _context.CustomFields.FindAsync(fieldId);
        if (customFieldset == null || customField == null) throw new Exception("Fieldset or Field not found");

        customFieldset.CustomFields.Add(customField);
        await _context.SaveChangesAsync();
    }
}
