namespace Tea_Store.Models
{
    public class TeaReview
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int UserID { get; set; }
        public int TeaID { get; set; }
        public float Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual User? User { get; set; }
        public virtual Tea? Tea { get; set; }
    }
}