namespace HelpdeskSystem.Application.DTOs;

public class TicketStatsDto
{
    public int TotalTickets { get; set; }
    public int OpenTickets { get; set; }
    public int InProgressTickets { get; set; }
    public int ResolvedTickets { get; set; }
    public int ClosedTickets { get; set; }
    public double AverageResolutionHours { get; set; }
    public List<AgentStatDto> AgentStats { get; set; } = new();
    public List<CategoryStatDto> CategoryStats { get; set; } = new();
}

public class AgentStatDto
{
    public string AgentName { get; set; } = string.Empty;
    public int AssignedCount { get; set; }
    public int ResolvedCount { get; set; }
}

public class CategoryStatDto
{
    public string CategoryName { get; set; } = string.Empty;
    public int Count { get; set; }
}