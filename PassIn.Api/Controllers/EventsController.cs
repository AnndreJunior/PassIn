using Microsoft.AspNetCore.Mvc;
using PassIn.Application;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RequestEventJson request)
    {
        var useCase = new RegisterEventsUseCase();

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var useCase = new GetEventByIdUseCase();

        var response = await useCase.Execute(id);

        return Ok(response);
    }
}
