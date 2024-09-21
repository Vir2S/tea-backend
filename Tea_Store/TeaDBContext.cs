﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tea_Store.Entities;

namespace Tea_Store
{
    internal class TeaDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SiteReview> SiteReviews { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTea> OrderTeas { get; set; }
        public DbSet<TeaReview> TeaReviews { get; set; }
        public DbSet<Tea> Teas { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentTea> ComponentTeas { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListTea> WishListTeas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("ConnectionStringHere");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - History (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.History)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.UserID);

            // User - WishList (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.WishList)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserID);

            // User - Order (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserID);

            // User - SiteReview (One-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.SiteReview)
                .WithOne(sr => sr.User)
                .HasForeignKey<SiteReview>(sr => sr.UserID);

            // User - TeaReview (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.TeaReviews)
                .WithOne(tr => tr.User)
                .HasForeignKey(tr => tr.UserID);

            // Tea - TeaReview (One-to-Many)
            modelBuilder.Entity<Tea>()
                .HasMany(t => t.TeaReviews)
                .WithOne(tr => tr.Tea)
                .HasForeignKey(tr => tr.TeaID);
            
            // Tea - OrderTea (One-to-Many)
            modelBuilder.Entity<Tea>()
                .HasMany(t => t.OrderTeas)
                .WithOne(ot => ot.Tea)
                .HasForeignKey(ot => ot.TeaID);
            
            // Tea - ComponentTea (One-to-Many)
            modelBuilder.Entity<Tea>()
                .HasMany(t => t.ComponentTeas)
                .WithOne(ct => ct.Tea)
                .HasForeignKey(ct => ct.TeaID);
            
            // Tea - WishListTea (One-to-Many)
            modelBuilder.Entity<Tea>()
                .HasMany(t => t.WishListTeas)
                .WithOne(wlt => wlt.Tea)
                .HasForeignKey(wlt => wlt.TeaID);

            // Order - OrderTea (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderTeas)
                .WithOne(ot => ot.Order)
                .HasForeignKey(ot => ot.OrderID);

            // Component - ComponentTea (One-to-Many)
            modelBuilder.Entity<Component>()
                .HasMany(c => c.ComponentTeas)
                .WithOne(ct => ct.Component)
                .HasForeignKey(ct => ct.ComponentID);

            // WishList - WishListTea (One-to-Many)
            modelBuilder.Entity<WishList>()
                .HasMany(wl => wl.WishListTeas)
                .WithOne(wlt => wlt.WishList)
                .HasForeignKey(wlt => wlt.WishListID);

            // Order - OrderTea (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderTeas)
                .WithOne(ot => ot.Order)
                .HasForeignKey(ot => ot.OrderID);

            // Order - History (One-to-One)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.History)
                .WithOne(h => h.Order)
                .HasForeignKey<Order>(o => o.HistoryID);

        }
    }
}
