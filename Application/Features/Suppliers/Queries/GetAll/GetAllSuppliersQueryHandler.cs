using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Suppliers.Queries.GetAll;
public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, IEnumerable<Supplier>>
{
    private readonly ISupplierRepository _supplierRepository;

    public GetAllSuppliersQueryHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        return await _supplierRepository.GetAllAsync();
    }
}
