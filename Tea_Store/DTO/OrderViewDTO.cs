﻿namespace Tea_Store.DTO
{
    public class OrderViewDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public List<TeaDTO> Teas { get; set; }
    }
}