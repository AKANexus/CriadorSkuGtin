// <auto-generated />
using System;
using CriadorSkuGtin.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CriadorSkuGtin.EntityFramework.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CriadorSkuGtin.Domain.Fabricante", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Abreviatura")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Descrição")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Uuid");

                    b.ToTable("Fabricantes");
                });

            modelBuilder.Entity("CriadorSkuGtin.Domain.Grupo", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Abreviatura")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Descrição")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Uuid");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("CriadorSkuGtin.Domain.NextGtinGenerator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Sequential")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GtinStorage");
                });

            modelBuilder.Entity("CriadorSkuGtin.Domain.StoredSku", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Gtin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Uuid");

                    b.ToTable("Skus");
                });
#pragma warning restore 612, 618
        }
    }
}
