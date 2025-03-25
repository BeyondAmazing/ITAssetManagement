using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Queries.GetAll;

public record GetAllCategoriesQuery : IRequest<IEnumerable<Category>> { }
