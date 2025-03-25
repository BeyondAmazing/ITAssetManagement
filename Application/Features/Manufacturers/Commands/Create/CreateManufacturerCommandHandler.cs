using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Manufacturers.Commands.Create;
public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, Manufacturer>
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public CreateManufacturerCommandHandler(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    public async Task<Manufacturer> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = Manufacturer.Create(request.Name);
        return await _manufacturerRepository.AddAsync(manufacturer);
    }
}
