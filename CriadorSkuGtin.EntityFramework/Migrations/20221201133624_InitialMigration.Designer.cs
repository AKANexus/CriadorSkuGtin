﻿// <auto-generated />
using System;
using CriadorSkuGtin.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CriadorSkuGtin.EntityFramework.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221201133624_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
#pragma warning restore 612, 618
        }
    }
}
