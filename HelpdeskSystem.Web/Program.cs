using HelpdeskSystem.Application.Interfaces;
using HelpdeskSystem.Infrastructure.Identity;
using HelpdeskSystem.Infrastructure.Persistence;
using HelpdeskSystem.Infrastructure.Repositories;
using HelpdeskSystem.Web;
using HelpdeskSystem.Web.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Application.Services;
using HelpdeskSystem.Web.Hubs;
using HelpdeskSystem.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
builder.Services.AddScoped<ITicketCommentRepository, TicketCommentRepository>();
builder.Services.AddScoped<ITicketStatsService, TicketStatsService>();
builder.Services.AddScoped<IPdfReportService, PdfReportService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSignalR();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapIdentityEndpoints();
app.MapReportEndpoints();
app.MapHub<NotificationHub>("/notificationHub");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await context.Database.MigrateAsync();
    await DbSeeder.SeedAsync(context, userManager, roleManager);
}

app.Run();