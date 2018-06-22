using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Zhiji.Organization.Api.Commands.Companies;
using Zhiji.Organization.Api.Controllers;
using Zhiji.Organization.Api.ViewModels;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Services.UnitTest.Organization
{
    public class CompaniesControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;

        private Company DummyCompany => new Company("dummy", null);
        private CompaniesController Controller => new CompaniesController(_mediatorMock.Object, _mapperMock.Object);

        public CompaniesControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetCompanyByIdReturnNotFound()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<QueryCompanyCommand>(), default))
                .Returns(Task.FromResult(Enumerable.Empty<Company>()));

            var result = await this.Controller.Get(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetCompanyByIdReturnOK()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<QueryCompanyCommand>(), default))
                .Returns(Task.FromResult(new[] { this.DummyCompany }.AsEnumerable()));

            _mapperMock
                .Setup(m => m.Map<CompanyViewModel>(It.IsAny<object>()))
                .Returns(new CompanyViewModel());

            var result = await this.Controller.Get(1);
            Assert.IsType<ActionResult<CompanyViewModel>>(result);
        }

        [Fact]
        public async Task GetAllCompaniesReturnOK()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<QueryCompanyCommand>(), default))
                .Returns(Task.FromResult(Enumerable.Empty<Company>()));

            _mapperMock
                .Setup(m => m.Map<IEnumerable<CompanyViewModel>>(It.IsAny<object>()))
                .Returns(Enumerable.Empty<CompanyViewModel>());

            var result = await this.Controller.GetAll();
            Assert.IsAssignableFrom<IEnumerable<CompanyViewModel>>(result);
        }

        [Fact]
        public async Task CreateCompanyReturnCreated()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateCompanyCommand>(), default))
                .Returns(Task.FromResult(this.DummyCompany));

            _mapperMock
                .Setup(m => m.Map<CompanyViewModel>(It.IsAny<object>()))
                .Returns(new CompanyViewModel());

            var result = await this.Controller.Create(new CreateCompanyCommand());            
            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}
