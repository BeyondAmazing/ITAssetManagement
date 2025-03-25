using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Consumables.Queries.GetAll;
public class GetAllConsumablesQueryHandler : IRequestHandler<GetAllConsumablesQuery, IEnumerable<Consumable>>
{
    private readonly IConsumableRepository _consumableRepository;

    public GetAllConsumablesQueryHandler(IConsumableRepository consumableRepository)
    {
        _consumableRepository = consumableRepository;
    }

    public async Task<IEnumerable<Consumable>> Handle(GetAllConsumablesQuery request, CancellationToken cancellationToken)
    {
        return await _consumableRepository.GetAllAsync();
    }
}
