using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Exceptions;
using PassIn.Infra;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _context;

    public RegisterAttendeeOnEventUseCase(PassInDbContext context)
    {
        _context = context;
    }

    public async Task Execute(Guid eventId, RequestRegisterEventJson request)
    {
        await Validate(eventId, request);

        var entity = new Infra.Entities.Attendee
        {
            Email = request.Email,
            Name = request.Name,
            Event_Id = eventId,
            Created_At = DateTime.UtcNow
        };

        await _context.Attendees.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    private async Task Validate(Guid eventId, RequestRegisterEventJson request)
    {
        var eventEntity = await _context.Events.FindAsync(eventId);
        if (eventEntity is null)
            throw new NotFoundException("Evento não encontrado");
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ErrorOnValidationException("Nome inválido");
        if (isEmailInvalid(request.Email))
            throw new ErrorOnValidationException("Email inválido");
        var attendeeIsAlreadyRegistered = await _context
            .Attendees
            .AnyAsync(at => at.Email.Equals(request.Email) && at.Event_Id == eventId);
        if (attendeeIsAlreadyRegistered)
            throw new ConflictException("Usuário já registrado em um evento");

        var attendeesForEvent = await _context.Attendees.CountAsync(at => at.Event_Id == eventId);

        if (attendeesForEvent == eventEntity.Maximum_Attendees)
            throw new ConflictException("Não há mais vagas para esse evento");
    }

    private bool isEmailInvalid(string email)
    {
        try
        {
            new MailAddress(email);

            return false;
        }
        catch
        {
            return true;
        }
    }
}
