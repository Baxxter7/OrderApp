using AutoMapper;
using OrderApp.Domain.DTOs;
using OrderApp.Domain.Entities;
using OrderApp.Domain.Enums;
using OrderApp.Domain.Interfaces;

namespace OrderApp.Business.Services;

public class OrderService 
{
    private  readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        if (createOrderDto.Items == null || !createOrderDto.Items.Any())
        {
            throw new ArgumentException("La orden debe tener al menos un item");
        }

        Order order = new Order()
        {
            CustomerName = createOrderDto.CostumerName,
            CustomerEmail = createOrderDto.CostumerEmail,
            OrderDate = DateTime.Now,
            Status = OrderStatus.Pending,
            Items = createOrderDto.Items.Select(item => new OrderItem
            {
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };

        //Logica del negocio: Calcular total
        order.TotalAmount = order.Items.Sum(item => item.Quantity * item.Price);
        
        if(order.TotalAmount > 100) order.TotalAmount *= 0.95m; //Calcular descuento del 5%

        var createdOrder = await _orderRepository.CreateAsync(order);
        return _mapper.Map<OrderResponseDto>(createdOrder);

    }

    public async Task<OrderResponseDto> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new KeyNotFoundException($"Orden con el ID {id} no encontrada");
        return _mapper.Map<OrderResponseDto>(order);
    }
    
    public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }
}