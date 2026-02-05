namespace queueless.Models
{
    public class Pedidos
    {
        public Pedidos(Guid userId, double total)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Total = total;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
