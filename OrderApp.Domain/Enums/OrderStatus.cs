namespace OrderApp.Domain.Enums;

public enum OrderStatus
{
    Pending = 1,
    Confirmed = 2,
    InPreparation = 3,
    Ready = 4,
    Delivered = 5,
    Cancelled = 6,
}