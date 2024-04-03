using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infra;

namespace PassIn.Application.UseCases.Events.GetById;

public class GetEventByIdUseCase
{
    public async Task<ResponseEventJson> Execute(Guid id)
    {
        var dbContext = new PassInDbContext();

        var entity = await dbContext.Events.FindAsync(id) ?? throw new PassInException("Evento não encontrado");

        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = -1
        };
    }
}
