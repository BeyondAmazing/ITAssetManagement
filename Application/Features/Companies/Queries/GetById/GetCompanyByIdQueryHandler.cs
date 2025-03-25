using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Companies.Queries.GetById;
public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Company?>
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Company?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        return await _companyRepository.GetByIdAsync(request.Id);
    }
}
