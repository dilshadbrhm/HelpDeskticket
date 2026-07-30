using Microsoft.AspNetCore.Identity;
using HelpdeskSystem.Domain.Enums;

namespace HelpdeskSystem.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Employee;
}