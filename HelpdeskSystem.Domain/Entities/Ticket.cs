using HelpdeskSystem.Domain.Enums;

namespace HelpdeskSystem.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;

    public int CategoryId { get; set; }
    public TicketCategory? Category { get; set; }

    public string CreatedByUserId { get; set; } = string.Empty;
    public string? AssignedAgentId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ResolvedAt { get; set; }

    public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
}