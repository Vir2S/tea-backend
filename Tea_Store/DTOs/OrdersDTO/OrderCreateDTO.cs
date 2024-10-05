namespace Tea_Store.DTOs.OrdersDTO
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public List<int> TeaIds { get; set; }
    }
}
