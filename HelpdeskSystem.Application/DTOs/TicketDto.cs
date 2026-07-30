using HelpdeskSystem.Domain.Enums;

namespace HelpdeskSystem.Application.DTOs;

public class TicketDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CreatedByUserId { get; set; } = string.Empty;
    public string? AssignedAgentId { get; set; }
    public DateTime CreatedAt { get; set; }
}