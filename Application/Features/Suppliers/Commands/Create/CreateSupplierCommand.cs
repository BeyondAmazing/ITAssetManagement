using Domain.Entities;
using MediatR;

namespace Application.Features.Suppliers.Commands.Create;

public record CreateSupplierCommand(string Name,string? address,string? phone,string? email) : IRequest<Supplier> { }
