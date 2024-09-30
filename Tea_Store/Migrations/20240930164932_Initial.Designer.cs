﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tea_Store.Data;

#nullable disable

namespace Tea_Store.Migrations
{
    [DbContext(typeof(TeaDBContext))]
    [Migration("20240930164932_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Tea_Store.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("Tea_Store.Models.ComponentTea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComponentID")
                        .HasColumnType("int");

                    b.Property<int>("TeaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComponentID");

                    b.HasIndex("TeaID");

                    b.ToTable("ComponentTeas");
                });

            modelBuilder.Entity("Tea_Store.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Tea_Store.Models.OrderTea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("TeaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderID");

                    b.HasIndex("TeaID");

                    b.ToTable("OrderTeas");
                });

            modelBuilder.Entity("Tea_Store.Models.SiteReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("SiteReviews");
                });

            modelBuilder.Entity("Tea_Store.Models.Tea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AgeLimits")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Teas");
                });

            modelBuilder.Entity("Tea_Store.Models.TeaReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("TeaID")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeaID");

                    b.HasIndex("UserID");

                    b.ToTable("TeaReviews");
                });

            modelBuilder.Entity("Tea_Store.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("WishListID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tea_Store.Models.WishList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("Tea_Store.Models.WishListTea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TeaID")
                        .HasColumnType("int");

                    b.Property<int>("WishListID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeaID");

                    b.HasIndex("WishListID");

                    b.ToTable("WishListTeas");
                });

            modelBuilder.Entity("Tea_Store.Models.ComponentTea", b =>
                {
                    b.HasOne("Tea_Store.Models.Component", "Component")
                        .WithMany("ComponentTeas")
                        .HasForeignKey("ComponentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tea_Store.Models.Tea", "Tea")
                        .WithMany("ComponentTeas")
                        .HasForeignKey("TeaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");

                    b.Navigation("Tea");
                });

            modelBuilder.Entity("Tea_Store.Models.Order", b =>
                {
                    b.HasOne("Tea_Store.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tea_Store.Models.OrderTea", b =>
                {
                    b.HasOne("Tea_Store.Models.Order", "Order")
                        .WithMany("OrderTeas")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tea_Store.Models.Tea", "Tea")
                        .WithMany("OrderTeas")
                        .HasForeignKey("TeaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Tea");
                });

            modelBuilder.Entity("Tea_Store.Models.SiteReview", b =>
                {
                    b.HasOne("Tea_Store.Models.User", "User")
                        .WithOne("SiteReview")
                        .HasForeignKey("Tea_Store.Models.SiteReview", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tea_Store.Models.TeaReview", b =>
                {
                    b.HasOne("Tea_Store.Models.Tea", "Tea")
                        .WithMany("TeaReviews")
                        .HasForeignKey("TeaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tea_Store.Models.User", "User")
                        .WithMany("TeaReviews")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tea");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tea_Store.Models.WishList", b =>
                {
                    b.HasOne("Tea_Store.Models.User", "User")
                        .WithOne("WishList")
                        .HasForeignKey("Tea_Store.Models.WishList", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tea_Store.Models.WishListTea", b =>
                {
                    b.HasOne("Tea_Store.Models.Tea", "Tea")
                        .WithMany("WishListTeas")
                        .HasForeignKey("TeaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tea_Store.Models.WishList", "WishList")
                        .WithMany("WishListTeas")
                        .HasForeignKey("WishListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tea");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("Tea_Store.Models.Component", b =>
                {
                    b.Navigation("ComponentTeas");
                });

            modelBuilder.Entity("Tea_Store.Models.Order", b =>
                {
                    b.Navigation("OrderTeas");
                });

            modelBuilder.Entity("Tea_Store.Models.Tea", b =>
                {
                    b.Navigation("ComponentTeas");

                    b.Navigation("OrderTeas");

                    b.Navigation("TeaReviews");

                    b.Navigation("WishListTeas");
                });

            modelBuilder.Entity("Tea_Store.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("SiteReview");

                    b.Navigation("TeaReviews");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("Tea_Store.Models.WishList", b =>
                {
                    b.Navigation("WishListTeas");
                });
#pragma warning restore 612, 618
        }
    }
}