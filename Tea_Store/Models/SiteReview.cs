using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tea_Store.Models
{
    internal class SiteReview
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int UserID { get; set; }
        public float Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual User? User { get; set; }
    }
}
