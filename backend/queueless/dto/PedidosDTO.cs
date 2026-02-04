namespace queueless.dto;

public record OrdersDto(
    Guid userId,
    double total
);