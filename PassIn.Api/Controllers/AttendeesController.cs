using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    [HttpPost("{eventId}/register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register(Guid eventId, RequestRegisterEventJson request)
    {
        var useCase = new RegisterAttendeeOnEvenvUseCase(new Infra.PassInDbContext());

        await useCase.Execute(eventId, request);

        return Created();
    }
}
