using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetAll;

public record GetAllUsersQuery : IRequest<IEnumerable<User>> { }
