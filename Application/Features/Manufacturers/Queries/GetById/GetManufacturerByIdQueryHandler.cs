using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Manufacturers.Queries.GetById;
public class GetManufacturerByIdQueryHandler : IRequestHandler<GetManufacturerByIdQuery, Manufacturer?>
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public GetManufacturerByIdQueryHandler(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    public async Task<Manufacturer?> Handle(GetManufacturerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _manufacturerRepository.GetByIdAsync(request.Id);
    }
}
