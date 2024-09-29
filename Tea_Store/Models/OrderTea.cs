﻿namespace Tea_Store.Models
{
    public class OrderTea
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int TeaID { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Tea? Tea { get; set; }
    }
}