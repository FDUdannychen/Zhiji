using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zhiji.Organization.Domain.Branches;

namespace Zhiji.Organization.Api.Commands.Branches
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, Branch>
    {
        private readonly IBranchRepository _branchRepository;

        public CreateBranchCommandHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<Branch> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = new Branch
            {
                Name = request.Name
            };

            _branchRepository.Add(branch);
            await _branchRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return branch;
        }
    }
}
