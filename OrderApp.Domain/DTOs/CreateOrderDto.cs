namespace OrderApp.Domain.DTOs;

public class CreateOrderDto
{
    public string CostumerName { get; set; } = string.Empty;
    public string CostumerEmail { get; set; } = string.Empty;
    public List<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
}