﻿// <auto-generated />
using FishingStoreApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FishingStoreApp.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240622094839_addImageUrlToProduct")]
    partial class addImageUrlToProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FishingStoreApp.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            DisplayOrder = 1,
                            Name = "Fishing Rods"
                        },
                        new
                        {
                            CategoryId = 2,
                            DisplayOrder = 2,
                            Name = "Fishing Lines"
                        },
                        new
                        {
                            CategoryId = 3,
                            DisplayOrder = 3,
                            Name = "Lures"
                        },
                        new
                        {
                            CategoryId = 4,
                            DisplayOrder = 4,
                            Name = "Storage"
                        });
                });

            modelBuilder.Entity("FishingStoreApp.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stocks")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Description = "A true all-around performer with the features and feel associated with rods four times the cost.",
                            ImageUrl = "",
                            Price = 369.99000000000001,
                            ProductCode = "ECHOCBXL690",
                            ProductName = "Echo Carbon XL Medium/Fast Fly Rod",
                            Stocks = 10
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            Description = "Despite the deceptively airy feel, the Abu Garcia Veritas 4.0 Spinning Surf Rod is robust, responsive and comfortable to fish with from dusk to dawn.",
                            ImageUrl = "",
                            Price = 229.99000000000001,
                            ProductCode = "1547516",
                            ProductName = "Abu Garcia Veritas 2 Piece Surf Rod",
                            Stocks = 10
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            Description = "A good reliable braid makes the difference between an average fishing trip and an exceptional one. We have proved just how reliable this top quality braid is and thoroughly recommend it. The strongest, most abrasion resistant superline in its class.",
                            ImageUrl = "",
                            Price = 239.99000000000001,
                            ProductCode = "119730-V",
                            ProductName = "Berkley Fireline Original Bulk 1300m Braid",
                            Stocks = 5
                        });
                });

            modelBuilder.Entity("FishingStoreApp.Models.Product", b =>
                {
                    b.HasOne("FishingStoreApp.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
