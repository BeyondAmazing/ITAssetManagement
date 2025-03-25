using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.Create;

public record CreateCategoryCommand(string Name) : IRequest<Category> { }
