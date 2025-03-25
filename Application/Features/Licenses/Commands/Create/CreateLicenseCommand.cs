using Domain.Entities;
using MediatR;

namespace Application.Features.Licenses.Commands.Create;

public record CreateLicenseCommand(
    string Name,
    string Serial,
    int Seats,
    Guid? CompanyId,
    Guid? SupplierId,
    DateTime? ExpirationDate,
    decimal PurchaseCost) : IRequest<License>;
