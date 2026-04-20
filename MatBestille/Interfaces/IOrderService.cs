namespace MatBestille.Interfaces;

public interface IOrderService
{
    //opprette en ny bestiling og lager den 
    Order CreateOrder(string customerId, string roomNumber, DateTime deliveryTime, List<OrderLine> lines);
    //hente alle bestiling for em kunde
    List<Order> GetOrdersByCustomer(string customerId);
    //hent alle bestiling fra ansatt
    List<Order> GetAllOrders();
    //hent senere bestilling
    List<Order> GetUpComingOrders();
    //merk bestiliing som har levert 
    void MarkAsDelivered(string orderId);
}