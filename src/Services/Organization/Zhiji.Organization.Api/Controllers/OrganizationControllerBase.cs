using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Organization.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class OrganizationControllerBase : Controller
    {
    }
}
