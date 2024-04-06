using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIn;
using PassIn.Communication.Responses;
using PassIn.Infra;

namespace PassIn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckInController : ControllerBase
{
    [HttpPost("{attendeeId:guid}")]
    [ProducesResponseType(typeof(ResponseRegisterEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckIn(Guid attendeeId)
    {
        var dbContext = new PassInDbContext();
        var useCase = new DoAttendeeCheckInUseCase(dbContext);

        var response = await useCase.Execute(attendeeId);

        return Created(string.Empty, response);
    }
}
