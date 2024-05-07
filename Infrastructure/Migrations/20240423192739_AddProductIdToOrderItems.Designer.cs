﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240423192739_AddProductIdToOrderItems")]
    partial class AddProductIdToOrderItems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Ordering.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("DatePlaced")
                        .HasColumnType("datetime2");

                    b.Property<int>("Discount")
                        .HasColumnType("int")
                        .HasColumnName("Discount");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float")
                        .HasColumnName("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Models.Ordering.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("Cantity")
                        .HasColumnType("int")
                        .HasColumnName("Cantity");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("Price");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Models.Roles.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Domain.Models.Roles.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("float(18)")
                        .HasDefaultValue(0.0)
                        .HasColumnName("Balance");

                    b.Property<string>("ConfirmationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ConfirmCode");

                    b.Property<bool>("Confirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Confirmed");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("FirstName");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("Phone");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("Domain.Models.Roles.Student", b =>
                {
                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<int>("AcademicYear")
                        .HasColumnType("int")
                        .HasColumnName("AcademicYear");

                    b.Property<int>("CurrentDegree")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Email");

                    b.Property<string>("MatrNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MatrNumber");

                    b.Property<string>("UniversityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BuyerId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.Models.Shopping.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("Domain.Models.Shopping.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Description");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsInStock")
                        .HasColumnType("bit");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("Price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Models.Shopping.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("DatePlaced")
                        .HasColumnType("datetime2");

                    b.Property<int>("Discount")
                        .HasColumnType("int")
                        .HasColumnName("Discount");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float")
                        .HasColumnName("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId")
                        .IsUnique();

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Domain.Models.Shopping.ShoppingCartItem", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.Property<int>("Cantity")
                        .HasColumnType("int")
                        .HasColumnName("Cantity");

                    b.HasKey("ProductId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingItems");
                });

            modelBuilder.Entity("Domain.Models.Ordering.Order", b =>
                {
                    b.HasOne("Domain.Models.Roles.Buyer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("Domain.Models.Ordering.OrderItem", b =>
                {
                    b.HasOne("Domain.Models.Ordering.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Shopping.Product", "Product")
                        .WithOne()
                        .HasForeignKey("Domain.Models.Ordering.OrderItem", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.Roles.Student", b =>
                {
                    b.HasOne("Domain.Models.Roles.Buyer", "Buyer")
                        .WithOne("Student")
                        .HasForeignKey("Domain.Models.Roles.Student", "BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("Domain.Models.Shopping.Coupon", b =>
                {
                    b.HasOne("Domain.Models.Roles.Buyer", "Buyer")
                        .WithMany("Coupons")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("Domain.Models.Shopping.ShoppingCart", b =>
                {
                    b.HasOne("Domain.Models.Roles.Buyer", "Buyer")
                        .WithOne()
                        .HasForeignKey("Domain.Models.Shopping.ShoppingCart", "BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("Domain.Models.Shopping.ShoppingCartItem", b =>
                {
                    b.HasOne("Domain.Models.Shopping.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Shopping.ShoppingCart", "ShoppingCart")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Domain.Models.Ordering.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Domain.Models.Roles.Buyer", b =>
                {
                    b.Navigation("Coupons");

                    b.Navigation("Student")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Shopping.ShoppingCart", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
