using Microsoft.EntityFrameworkCore;
using queueless.Data;
using queueless.Models;
using queueless.dto;

namespace queueless.Routes;

public static class OrdersRoute
{
    public static void OrdersRoutes(this WebApplication app)
    {
        var route = app.MapGroup("api/orders");

        // GET ALL ORDERS
        route.MapGet("/",
        async (QueuelessContext context) =>
        {
            var orders = await context.Orders.ToListAsync();
            return Results.Ok(orders);
        });

        // GET ORDER BY ID
        route.MapGet("/{id}",
        async (Guid id, QueuelessContext context) =>
        {
            var order = await context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Results.NotFound();

            var items = await context.OrderItems
                .Where(i => i.OrderId == id)
                .ToListAsync();

            return Results.Ok(new { order, items });
        });

        // CREATE ORDER + ITEMS
        route.MapPost("/",
        async (QueuelessContext context, CreateOrderDto req) =>
        {
            var order = new Pedidos(
                req.userId,
                req.items.Sum(i => i.quantity * i.unitPrice)
            );

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            foreach (var item in req.items)
            {
                var orderItem = new ItensPedidos(
                    order.Id,
                    item.productId,
                    item.quantity,
                    item.unitPrice
                );

                context.OrderItems.Add(orderItem);
            }

            await context.SaveChangesAsync();

            return Results.Ok(order);
        });

        route.MapPut("/{id}",
        async (Guid id, CreateOrderDto dto, QueuelessContext context) =>
        {
            var order = await context.Orders.FindAsync(id);

            if (order == null)
                return Results.NotFound();

            order.Total = dto.items.Sum(i => i.quantity * i.unitPrice);

            await context.SaveChangesAsync();
            return Results.Ok(order);
        });
    }
}