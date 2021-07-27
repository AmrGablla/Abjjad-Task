using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private IAuthenticatedUserService _authenticatedUserService;
        protected IAuthenticatedUserService AuthenticatedUserService => _authenticatedUserService ??= HttpContext.RequestServices.GetService<IAuthenticatedUserService>();
    }
}
