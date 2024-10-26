namespace Tea_Store.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int TeaId { get; set; }
        public int Quantity { get; set; }
        public virtual Tea? Tea { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}
