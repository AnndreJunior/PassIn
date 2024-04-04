using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infra;

namespace PassIn.Application;

public class RegisterEventsUseCase
{
    public async Task<ResponseRegisterEventJson> Execute(RequestEventJson request)
    {
        Validate(request);

        var dbContext = new PassInDbContext();

        var newEvent = new Infra.Entities.Event
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(' ', '-')
        };

        await dbContext.Events.AddAsync(newEvent);
        await dbContext.SaveChangesAsync();

        return new ResponseRegisterEventJson
        {
            Id = newEvent.Id
        };
    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
            throw new ErrorOnValidationException("O número máximo de participantes é inválido");
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ErrorOnValidationException("Título do evento inválido");
        if (string.IsNullOrWhiteSpace(request.Details))
            throw new ErrorOnValidationException("Detalhes inválidos");
    }
}
