using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Categories.Commands.Create;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name);
        return await _categoryRepository.AddAsync(category);
    }
}
