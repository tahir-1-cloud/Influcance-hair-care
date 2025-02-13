﻿// <auto-generated />
using System;
using InfluanceHairCare.models.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfluanceHairCare.models.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20230220071823_ratingss")]
    partial class ratingss
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InfluanceHairCare.models.Broadcast", b =>
                {
                    b.Property<int>("BroadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BroadId"), 1L, 1);

                    b.Property<bool>("AllCustomers")
                        .HasColumnType("bit");

                    b.Property<bool>("AllSaleRep")
                        .HasColumnType("bit");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("SaleRepId")
                        .HasColumnType("int");

                    b.Property<string>("SelectCity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("SelectState")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BroadId");

                    b.ToTable("Broadcasts");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CustomerImage")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CustomerImagePath")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<long>("LoginId")
                        .HasColumnType("bigint");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("SaleRepID")
                        .HasColumnType("int");

                    b.Property<string>("Salon_Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("LoginId")
                        .IsUnique();

                    b.HasIndex("SaleRepID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Login", b =>
                {
                    b.Property<long>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LoginId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("LoginId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("InfluanceHairCare.models.OrderPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("OrdersId")
                        .HasColumnType("bigint");

                    b.Property<float>("PaymentAmount")
                        .HasColumnType("real");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("OrdersId");

                    b.ToTable("OrderPayments");
                });

            modelBuilder.Entity("InfluanceHairCare.models.OrderProducts", b =>
                {
                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("TotalCost")
                        .HasColumnType("float");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Orders", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<double>("GrandTotal")
                        .HasColumnType("float");

                    b.Property<int>("OrderBy")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("InfluanceHairCare.models.ProductRating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"), 1L, 1);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<DateTime>("RatingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RatingId");

                    b.ToTable("ProductRatings");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Products", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LowStockthreshold")
                        .HasColumnType("int");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductTags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductTrackingNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductWaight")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<float?>("RegularPrice")
                        .HasColumnType("real");

                    b.Property<string>("StockStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Visibility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("price")
                        .HasColumnType("real");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("InfluanceHairCare.models.SaleRep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Discount")
                        .HasColumnType("real");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("LoginId")
                        .HasColumnType("bigint");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("SaleRepImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaleRepImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("LoginId")
                        .IsUnique();

                    b.ToTable("SaleReps");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Customer", b =>
                {
                    b.HasOne("InfluanceHairCare.models.Login", "Login")
                        .WithOne("Customer")
                        .HasForeignKey("InfluanceHairCare.models.Customer", "LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfluanceHairCare.models.SaleRep", "SaleRep")
                        .WithMany("SaleRepCustomer")
                        .HasForeignKey("SaleRepID");

                    b.Navigation("Login");

                    b.Navigation("SaleRep");
                });

            modelBuilder.Entity("InfluanceHairCare.models.OrderPayment", b =>
                {
                    b.HasOne("InfluanceHairCare.models.Orders", "Orders")
                        .WithMany("OrderPayments")
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("InfluanceHairCare.models.OrderProducts", b =>
                {
                    b.HasOne("InfluanceHairCare.models.Orders", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfluanceHairCare.models.Products", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Orders", b =>
                {
                    b.HasOne("InfluanceHairCare.models.Customer", "Customer")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("InfluanceHairCare.models.SaleRep", b =>
                {
                    b.HasOne("InfluanceHairCare.models.Login", "Login")
                        .WithOne("SaleRep")
                        .HasForeignKey("InfluanceHairCare.models.SaleRep", "LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Customer", b =>
                {
                    b.Navigation("CustomerOrders");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Login", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("SaleRep");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Orders", b =>
                {
                    b.Navigation("OrderPayments");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("InfluanceHairCare.models.Products", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("InfluanceHairCare.models.SaleRep", b =>
                {
                    b.Navigation("SaleRepCustomer");
                });
#pragma warning restore 612, 618
        }
    }
}
