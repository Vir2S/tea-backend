﻿using Tea_Store.Entities;

internal class User
{
    public int Id { get; set; }
    public string FName { get; set; } = null!;
    public string Sname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Age { get; set; }
    public byte[] Photo { get; set; } = null!;
    public int WishListID { get; set; }
    public int HistoryID { get; set; }

    public virtual SiteReview? SiteReview { get; set; }
    public virtual ICollection<History>? History { get; set; }
    public virtual ICollection<WishList>? WishList { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual ICollection<TeaReview>? TeaReviews { get; set; }
}