namespace HelpdeskSystem.Domain.Entities;

public class TicketComment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }

    public string AuthorUserId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}