using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.Create
{
    public record CreateUserCommand(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    Guid? CompanyId) : IRequest<User>;
}
