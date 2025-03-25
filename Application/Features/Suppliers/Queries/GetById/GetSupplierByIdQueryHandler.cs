using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Suppliers.Queries.GetById;
public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, Supplier?>
{
    private readonly ISupplierRepository _supplierRepository;

    public GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<Supplier?> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        return await _supplierRepository.GetByIdAsync(request.Id);
    }
}
