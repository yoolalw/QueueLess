namespace queueless.Models
{
    public class Produtos
    {
        public Produtos(string name, double price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            IsActive = true;
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
