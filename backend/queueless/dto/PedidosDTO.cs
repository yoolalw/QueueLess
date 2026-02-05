namespace queueless.dto;

public class CreateOrderDto
{
    public Guid userId { get; set; }
    public List<ordersItensDTO> items { get; set; } = new();
}
