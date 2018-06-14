using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Organization.Api.Commands.Companies
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Company>
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company(request.Name, request.ParentId);

            _companyRepository.Add(company);
            await _companyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return company;
        }
    }
}
