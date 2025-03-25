using Domain.Entities;

namespace Domain.Interfaces;

public interface ICustomFieldSetRepository : IGenericRepository<CustomFieldSet>
{
    Task AddFieldToSetAsync(Guid fieldsetId, Guid fieldId);
}