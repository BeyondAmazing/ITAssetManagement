using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Consumables.Commands.Create;
public class CreateConsumableCommandHandler : IRequestHandler<CreateConsumableCommand, Consumable>
{
    private readonly IConsumableRepository _consumableRepository;

    public CreateConsumableCommandHandler(IConsumableRepository consumableRepository)
    {
        _consumableRepository = consumableRepository;
    }
    public async Task<Consumable> Handle(CreateConsumableCommand request, CancellationToken cancellationToken)
    {
        var consumable = Consumable.Create(request.Name, request.CategoryId, request.CompanyId, request.Quantity, request.purchaseCost);
        return await _consumableRepository.AddAsync(consumable);
    }
}
