using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Depreciations.Commands.Create;
public class CreateDepreciationCommandHandler : IRequestHandler<CreateDepreciationCommand, Depreciation>
{
    private readonly IDepreciationRepository _depreciationRepository;

    public CreateDepreciationCommandHandler(IDepreciationRepository depreciationRepository)
    {
        _depreciationRepository = depreciationRepository;
    }

    public async Task<Depreciation> Handle(CreateDepreciationCommand request, CancellationToken cancellationToken)
    {
        var depreciation = Depreciation.Create(request.Name, request.Months);
        return await _depreciationRepository.AddAsync(depreciation);
    }
}
