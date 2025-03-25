using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Manufacturers.Queries.GetAll;
class GetAllManufacturerQueryHandler : IRequestHandler<GetAllManufacturersQuery, IEnumerable<Manufacturer>>
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public GetAllManufacturerQueryHandler(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    public async Task<IEnumerable<Manufacturer>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
    {
        return await _manufacturerRepository.GetAllAsync();
    }
}
