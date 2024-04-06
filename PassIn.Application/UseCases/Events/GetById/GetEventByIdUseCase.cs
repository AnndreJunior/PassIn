using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infra;

namespace PassIn.Application.UseCases.Events.GetById;

public class GetEventByIdUseCase
{
    public async Task<ResponseEventJson> Execute(Guid id)
    {
        var dbContext = new PassInDbContext();

        var entity = await dbContext.Events
            .Include(ev => ev.Attendees)
            .FirstOrDefaultAsync(ev => ev.Id == id) ?? throw new NotFoundException("Evento não encontrado");

        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = entity.Attendees.Count()
        };
    }
}
