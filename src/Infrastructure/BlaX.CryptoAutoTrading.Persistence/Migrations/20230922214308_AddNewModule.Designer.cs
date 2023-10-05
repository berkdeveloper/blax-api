﻿// <auto-generated />
using System;
using BlaX.CryptoAutoTrading.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlaX.CryptoAutoTrading.Persistence.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230922214308_AddNewModule")]
    partial class AddNewModule
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlaX.CryptoAutoTrading.Domain.Entities.TradingLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("ProfitRate")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("SaleDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("SalePrice")
                        .HasColumnType("double precision");

                    b.Property<int>("StatusType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("TradingLog");
                });

            modelBuilder.Entity("BlaX.CryptoAutoTrading.Domain.Entities.UserWallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("AmountMoneyDeposited")
                        .HasColumnType("double precision");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Earning")
                        .HasColumnType("double precision");

                    b.Property<double>("Loss")
                        .HasColumnType("double precision");

                    b.Property<int>("PaymentStatusType")
                        .HasColumnType("integer");

                    b.Property<double>("ProfitRate")
                        .HasColumnType("double precision");

                    b.Property<int>("StatusType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserWallet");
                });
#pragma warning restore 612, 618
        }
    }
}
