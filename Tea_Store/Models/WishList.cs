namespace Tea_Store.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public int UserID { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<WishListTea>? WishListTeas { get; set; }
    }
}