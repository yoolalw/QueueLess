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

        route.MapPost("/",
        async (QueuelessContext context,
               OrdersDto orderDto,
               List<ordersItensDTO> itemsDto) =>
        {
            var total = itemsDto.Sum(i => i.quantity * i.unitPrice);

            var order = new Pedidos(orderDto.userId, total);

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            foreach (var item in itemsDto)
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

        route.MapGet("/",
        async (QueuelessContext context) =>
        {
            var orders = await context.Orders.ToListAsync();
            return Results.Ok(orders);
        });
    }
}