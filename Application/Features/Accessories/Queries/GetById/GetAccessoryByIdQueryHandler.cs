using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Accessories.Queries.GetById;
public class GetAccessoryByIdQueryHandler : IRequestHandler<GetAccessoryByIdQuery, Accessory?>
{
    private readonly IAccessoryRepository _accessoryRepository;

    public GetAccessoryByIdQueryHandler(IAccessoryRepository accessoryRepository)
    {
        _accessoryRepository = accessoryRepository;
    }

    public async Task<Accessory?> Handle(GetAccessoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _accessoryRepository.GetByIdAsync(request.Id);
    }
}
