using Domain.Entities;
using MediatR;

namespace Application.Features.Assets.Queries.GetAll;

public record GetAllAssetsQuery : IRequest<IEnumerable<Asset>>;