using OrderApp.Domain.Enums;
namespace OrderApp.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string CostumerName { get; set; } = string.Empty;
    public string CostumerEmail { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}