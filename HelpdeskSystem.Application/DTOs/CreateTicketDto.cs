using HelpdeskSystem.Domain.Enums;

namespace HelpdeskSystem.Application.DTOs;

public class CreateTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public int CategoryId { get; set; }
    public string CreatedByUserId { get; set; } = string.Empty;
}