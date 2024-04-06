using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAll;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Infra;

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
        var useCase = new RegisterAttendeeOnEventUseCase(new Infra.PassInDbContext());

        await useCase.Execute(eventId, request);

        return Created();
    }

    [HttpGet("{eventId}")]
    [ProducesResponseType(typeof(ResponseAllAttendeesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetAll(Guid eventId)
    {
        var dbContext = new PassInDbContext();
        var useCase = new GetAllAttendeesByEventIdUseCase(dbContext);

        var response = useCase.Execute(eventId);

        return Ok(response);
    }
}
