using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Accessories.Commands.Create;

public class CreateAccessoryCommandHandler : IRequestHandler<CreateAccessoryCommand, Accessory>
{
    private readonly IAccessoryRepository _accessoryRepository;

    public CreateAccessoryCommandHandler(IAccessoryRepository accessoryRepository)
    {
        _accessoryRepository = accessoryRepository;
    }

    public async Task<Accessory> Handle(CreateAccessoryCommand request, CancellationToken cancellationToken)
    {
        var accessory = Accessory.Create(request.Name, request.CategoryId, request.CompanyId, request.ManufacturerId, request.Quantity, request.purchaseDate, request.purchaseCost);
        return await _accessoryRepository.AddAsync(accessory);
    }
}
