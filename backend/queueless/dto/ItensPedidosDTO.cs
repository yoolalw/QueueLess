namespace queueless.dto;

public record ordersItensDTO(
    Guid orderId,
    Guid productId,
    int quantity,
    double unitPrice
);