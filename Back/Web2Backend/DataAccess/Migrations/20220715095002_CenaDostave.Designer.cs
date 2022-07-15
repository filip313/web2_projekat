﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220715095002_CenaDostave")]
    partial class CenaDostave
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Models.Porudzbina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Cena")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal>("CenaDostave")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("DostavljacId")
                        .HasColumnType("int");

                    b.Property<string>("Komentar")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("NarucilacId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("CekaDostavu");

                    b.Property<long>("TrajanjeDostave")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("VremePrihvata")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DostavljacId");

                    b.HasIndex("NarucilacId");

                    b.ToTable("Porudzbine");
                });

            modelBuilder.Entity("DataLayer.Models.PorudzbinaProizvod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Kolicina")
                        .HasColumnType("bigint");

                    b.Property<int>("PorudzbinaId")
                        .HasColumnType("int");

                    b.Property<int>("ProizvodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("PorudzbinaId", "ProizvodId");

                    b.HasIndex("ProizvodId");

                    b.ToTable("PorudzbinaProizvod");
                });

            modelBuilder.Entity("DataLayer.Models.Proizvod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cena")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Naziv")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Sastojci")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("Naziv")
                        .IsUnique()
                        .HasFilter("[Naziv] IS NOT NULL");

                    b.ToTable("Proizvodi");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DatumRodjenja")
                        .IsRequired()
                        .HasColumnType("nvarchar(48)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Ime")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Prezime")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Slika")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Verifikovan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataLayer.Models.Porudzbina", b =>
                {
                    b.HasOne("DataLayer.Models.User", "Dostavljac")
                        .WithMany("Dostave")
                        .HasForeignKey("DostavljacId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.User", "Narucialc")
                        .WithMany("Porudzbine")
                        .HasForeignKey("NarucilacId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Dostavljac");

                    b.Navigation("Narucialc");
                });

            modelBuilder.Entity("DataLayer.Models.PorudzbinaProizvod", b =>
                {
                    b.HasOne("DataLayer.Models.Porudzbina", "Porudzbina")
                        .WithMany("Proizvodi")
                        .HasForeignKey("PorudzbinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.Proizvod", "Proizvod")
                        .WithMany("Porudzbine")
                        .HasForeignKey("ProizvodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Porudzbina");

                    b.Navigation("Proizvod");
                });

            modelBuilder.Entity("DataLayer.Models.Porudzbina", b =>
                {
                    b.Navigation("Proizvodi");
                });

            modelBuilder.Entity("DataLayer.Models.Proizvod", b =>
                {
                    b.Navigation("Porudzbine");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Navigation("Dostave");

                    b.Navigation("Porudzbine");
                });
#pragma warning restore 612, 618
        }
    }
}
