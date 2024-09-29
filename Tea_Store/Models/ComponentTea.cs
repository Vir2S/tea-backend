namespace Tea_Store.Models
{
    public class ComponentTea
    {
        public int Id { get; set; }
        public int ComponentID { get; set; }
        public int TeaID { get; set; }
        public virtual Component? Component { get; set; }
        public virtual Tea? Tea { get; set; }
    }
}