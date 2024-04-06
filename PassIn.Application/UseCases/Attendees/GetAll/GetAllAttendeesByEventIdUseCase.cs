using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infra;

namespace PassIn.Application.UseCases.Attendees.GetAll;

public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _context;

    public GetAllAttendeesByEventIdUseCase(PassInDbContext context)
    {
        _context = context;
    }

    public ResponseAllAttendeesJson Execute(Guid eventId)
    {
        var entity = _context.Events
            .Include(ev => ev.Attendees)
            .ThenInclude(attendee => attendee.CheckIn)
            .FirstOrDefault(ev => ev.Id == eventId)
            ?? throw new NotFoundException("Nenhum evento encontrado");

        return new ResponseAllAttendeesJson
        {
            Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
                CheckedInAt = attendee.CheckIn?.Created_at
            }).ToList()
        };
    }
}
