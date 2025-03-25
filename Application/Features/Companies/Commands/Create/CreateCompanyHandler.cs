using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Companies.Commands.Create;

public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, Company>
{
    private readonly ICompanyRepository _companyRepository;

    public CreateCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = Company.Create(request.name, request.email, request.phone);
        return await _companyRepository.AddAsync(company);
    }
}
