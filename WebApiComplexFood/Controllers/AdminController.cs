using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.DtoModels.Admin;
using Application.Features.Admins.Commands;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator, ILogger<AdminController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // POST: CreateAdmin
        [HttpPost("/Admin")]
        public async Task<ActionResult<AdminDto>> CreateAdmin([FromBody] AdminDto newAdmin)
        {
            //aici ar trebui verificat rolul, adica doar Adminul poate face un admin
            var command = new CreateAdminCommand
            {
                Admin = newAdmin
            };
            var admin = await _mediator.Send(command);

            return CreatedAtRoute(new { email = admin.Email }, admin);
        }
    }
}
