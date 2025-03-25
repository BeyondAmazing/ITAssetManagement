using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Components.Commands.Create;
public class CreateComponentCommandHandler : IRequestHandler<CreateComponentCommand, Component>
{
    private readonly IComponentRepository _componentRepository;

    public CreateComponentCommandHandler(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    public async Task<Component> Handle(CreateComponentCommand request, CancellationToken cancellationToken)
    {
        var component = Component.Create(request.Name, request.CategoryId, request.CompanyId, request.serial, request.Quantity, request.purchaseCost);
        return await _componentRepository.AddAsync(component);
    }
}
