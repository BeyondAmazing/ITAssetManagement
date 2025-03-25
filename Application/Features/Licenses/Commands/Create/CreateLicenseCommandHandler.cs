using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Licenses.Commands.Create;

public class CreateLicenseCommandHandler : IRequestHandler<CreateLicenseCommand, License>
{
    private readonly ILicenseRepository _licenseRepository;

    public CreateLicenseCommandHandler(ILicenseRepository licenseRepository)
    {
        _licenseRepository = licenseRepository;
    }

    public async Task<License> Handle(CreateLicenseCommand request, CancellationToken cancellationToken)
    {
        var license = License.Create(request.Name, request.Serial, request.Seats, request.CompanyId,
            request.SupplierId, request.ExpirationDate, request.PurchaseCost);
        return await _licenseRepository.AddAsync(license);
    }
}
