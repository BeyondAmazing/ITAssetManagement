using Domain.Entities;
using MediatR;

namespace Application.Features.Suppliers.Queries.GetById;

public record GetSupplierByIdQuery(Guid Id) : IRequest<Supplier?> { }
