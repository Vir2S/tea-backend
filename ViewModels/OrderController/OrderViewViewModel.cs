namespace ViewModels.OrderController
{
    public class OrderViewViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public List<TeaViewModel> Teas { get; set; }
    }
}