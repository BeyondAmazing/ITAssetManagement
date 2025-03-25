using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Suppliers.Commands.Create;
public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Supplier>
{
    private readonly ISupplierRepository _supplierRepository;

    public CreateSupplierCommandHandler(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<Supplier> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = Supplier.Create(request.Name, request.address, request.phone, request.email);
        return await _supplierRepository.AddAsync(supplier);
    }
}
