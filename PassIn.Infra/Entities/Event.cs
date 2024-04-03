namespace PassIn.Infra.Entities;

public class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int Maximum_Attendees { get; set; }
}
