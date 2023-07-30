﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prueba;

#nullable disable

namespace Prueba.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230726182028_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoriaNotaNota", b =>
                {
                    b.Property<int>("CategoriasNotaId")
                        .HasColumnType("int");

                    b.Property<int>("NotasId")
                        .HasColumnType("int");

                    b.HasKey("CategoriasNotaId", "NotasId");

                    b.HasIndex("NotasId");

                    b.ToTable("CategoriaNotaNota");
                });

            modelBuilder.Entity("Prueba.Entidades.CategoriaNota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CategoriasNota");
                });

            modelBuilder.Entity("Prueba.Entidades.Nota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("CategoriaNotaNota", b =>
                {
                    b.HasOne("Prueba.Entidades.CategoriaNota", null)
                        .WithMany()
                        .HasForeignKey("CategoriasNotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prueba.Entidades.Nota", null)
                        .WithMany()
                        .HasForeignKey("NotasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
