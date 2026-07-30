using Microsoft.AspNetCore.Identity;
using HelpdeskSystem.Infrastructure.Identity;

namespace HelpdeskSystem.Web;

public static class IdentityEndpoints
{
    public static void MapIdentityEndpoints(this WebApplication app)
    {
        app.MapPost("/Account/Login", async (
            HttpContext httpContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager) =>
        {
            var form = httpContext.Request.Form;
            var email = form["email"].ToString();
            var password = form["password"].ToString();
            var returnUrl = form["returnUrl"].ToString();

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Results.Redirect("/login?error=1");
            }

            var result = await signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Results.Redirect(returnUrl);
            }

            return Results.Redirect("/login?error=1");
        });

        app.MapPost("/Account/Logout", async (SignInManager<ApplicationUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Redirect("/login");
        });
    }
}