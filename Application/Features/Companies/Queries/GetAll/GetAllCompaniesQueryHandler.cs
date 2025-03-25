using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Companies.Queries.GetAll;
public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<Company>>
{
    private readonly ICompanyRepository _companyRepository;

    public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<IEnumerable<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await _companyRepository.GetAllAsync();
    }
}
