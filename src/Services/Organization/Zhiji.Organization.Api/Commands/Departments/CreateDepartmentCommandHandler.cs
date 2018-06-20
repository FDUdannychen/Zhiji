using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Api.Commands.Departments
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Department>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new Department(request.Name, request.ParentId, request.CompanyId);

            _departmentRepository.Add(department);
            await _departmentRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return department;
        }
    }
}
