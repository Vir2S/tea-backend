namespace Tea_Store.DTO
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public List<int> TeaIds { get; set; }
    }
}
