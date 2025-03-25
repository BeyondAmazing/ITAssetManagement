using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.ById;

public record GetUserByIdQuery(Guid Id) : IRequest<User?>;