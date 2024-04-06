using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infra;

namespace PassIn.Application.UseCases.CheckIn;

public class DoAttendeeCheckInUseCase
{
    private readonly PassInDbContext _context;

    public DoAttendeeCheckInUseCase(PassInDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseRegisterEventJson> Execute(Guid attendeeId)
    {
        Validate(attendeeId);

        var entity = new Infra.Entities.CheckIn
        {
            Id = Guid.NewGuid(),
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow
        };

        await _context.CheckIns.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new ResponseRegisterEventJson
        {
            Id = entity.Id
        };
    }

    private void Validate(Guid attendeeId)
    {
        var attendeeDoesNotExists = _context.Attendees.Any(attendee => attendee.Id == attendeeId) == false;
        if (attendeeDoesNotExists)
            throw new NotFoundException("Participante não encontrado");

        var checkInExists = _context.CheckIns.Any(checkIn => checkIn.Attendee_Id == attendeeId);
        if (checkInExists)
            throw new ConflictException("Participante não pode fazer check in duas vezes no mesmo evento");
    }
}
