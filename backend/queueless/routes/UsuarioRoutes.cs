using Microsoft.EntityFrameworkCore;
using queueless.Data;
using queueless.Models;
using queueless.dto;

namespace queueless.Routes;

public static class UsersRoute
{
    public static void UsersRoutes(this WebApplication app)
    {
        var route = app.MapGroup("api");

        // LOGIN
        route.MapPost("/Login",
        async (QueuelessContext context, LoginRequest req) =>
        {
            var user = await context.Users
                .FirstOrDefaultAsync(x =>
                    x.Username == req.username &&
                    x.Password == req.password &&
                    x.IsActive);

            if (user == null)
                return Results.BadRequest(new { message = "Usuário ou senha incorretos!" });

            return Results.Ok(new
            {
                user.Id,
                user.Name,
                Role = user.Role.ToString()
            });
        });

        // Criar usuário
        route.MapPost("/users",
        async (QueuelessContext context, UserDto dto) =>
        {
            var role = Enum.Parse<Usuarios.UserRole>(dto.role, true);

            var user = new Usuarios(dto.name, dto.username, dto.password, role);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Results.Ok(user);
        });

        // Listar usuários ativos
        route.MapGet("/users",
        async (QueuelessContext context) =>
        {
            var users = await context.Users
                .Where(x => x.IsActive)
                .ToListAsync();

            return Results.Ok(users);
        });

        // Soft delete usuário
        route.MapPatch("/users/{id}/deactivate",
        async (Guid id, QueuelessContext context) =>
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
                return Results.NotFound();

            user.IsActive = false;
            await context.SaveChangesAsync();

            return Results.Ok();
        });
    }
}