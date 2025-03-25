using Domain.Entities;
using MediatR;

namespace Application.Features.Suppliers.Queries.GetAll;

public record GetAllSuppliersQuery : IRequest<IEnumerable<Supplier>> { }
