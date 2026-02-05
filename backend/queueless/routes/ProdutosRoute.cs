using Microsoft.EntityFrameworkCore;
using queueless.Data;
using queueless.Models;
using queueless.dto;

namespace queueless.Routes;

public static class ProductsRoute
{
    public static void ProductsRoutes(this WebApplication app)
    {
        var route = app.MapGroup("api/products");

        route.MapGet("/",
        async (QueuelessContext context) =>
        {
            var products = await context.Products
                .Where(p => p.IsActive)
                .ToListAsync();

            return Results.Ok(products);
        });

        route.MapPost("/",
        async (QueuelessContext context, ProductsDto dto) =>
        {
            var product = new Produtos(dto.name, dto.price);

            context.Products.Add(product);
            await context.SaveChangesAsync();

            return Results.Ok(product);
        });

        route.MapPut("/{id}",
        async (Guid id, ProductsDto dto, QueuelessContext context) =>
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
                return Results.NotFound();

            product.Name = dto.name;
            product.Price = dto.price;

            await context.SaveChangesAsync();
            return Results.Ok(product);
        });

        // Soft delete
        route.MapPatch("/{id}/deactivate",
        async (Guid id, QueuelessContext context) =>
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
                return Results.NotFound();

            product.IsActive = false;

            await context.SaveChangesAsync();
            return Results.Ok();
        });
    }
}