﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240728142931_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Comentarios.Comentario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AutorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Destacado")
                        .HasColumnType("boolean");

                    b.Property<Guid>("HiloId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("texto");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("HiloId");

                    b.ToTable("comentarios", (string)null);
                });

            modelBuilder.Entity("Domain.Encuestas.Encuesta", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.HasKey("Id");

                    b.ToTable("encuestas", (string)null);
                });

            modelBuilder.Entity("Domain.Encuestas.Respuesta", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EncuestaId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EncuestaId");

                    b.ToTable("respuestas", (string)null);
                });

            modelBuilder.Entity("Domain.Encuestas.Voto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EncuestaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RespuestaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VotanteId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EncuestaId");

                    b.HasIndex("RespuestaId");

                    b.HasIndex("VotanteId");

                    b.ToTable("votos", (string)null);
                });

            modelBuilder.Entity("Domain.Hilos.Hilo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AutorId")
                        .HasColumnType("uuid")
                        .HasColumnName("autor_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("decripcion");

                    b.Property<Guid?>("EncuestaId")
                        .HasColumnType("uuid")
                        .HasColumnName("encuesta_id");

                    b.Property<Guid>("PortadaId")
                        .HasColumnType("uuid")
                        .HasColumnName("portada_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("titulo");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("EncuestaId")
                        .IsUnique();

                    b.HasIndex("PortadaId")
                        .IsUnique();

                    b.ToTable("hilos", (string)null);
                });

            modelBuilder.Entity("Domain.Media.MediaReference", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Spoiler")
                        .HasColumnType("boolean")
                        .HasColumnName("spoiler");

                    b.HasKey("Id");

                    b.ToTable("medias_references", (string)null);
                });

            modelBuilder.Entity("Domain.Usuarios.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("Rango")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("usuarios", (string)null);

                    b.HasDiscriminator<int>("Rango");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Usuarios.Anonimo", b =>
                {
                    b.HasBaseType("Domain.Usuarios.Usuario");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Domain.Usuarios.Moderador", b =>
                {
                    b.HasBaseType("Domain.Usuarios.Usuario");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Domain.Comentarios.Comentario", b =>
                {
                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Hilos.Hilo", null)
                        .WithMany("Comentarios")
                        .HasForeignKey("HiloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Comentarios.InformacionComentario", "Informacion", b1 =>
                        {
                            b1.Property<Guid>("ComentarioId")
                                .HasColumnType("uuid");

                            b1.HasKey("ComentarioId");

                            b1.ToTable("comentarios");

                            b1.WithOwner()
                                .HasForeignKey("ComentarioId");

                            b1.OwnsOne("Domain.Comentarios.ValueObjects.Dados", "Dados", b2 =>
                                {
                                    b2.Property<Guid>("InformacionComentarioComentarioId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Value")
                                        .HasColumnType("integer")
                                        .HasColumnName("dados");

                                    b2.HasKey("InformacionComentarioComentarioId");

                                    b2.ToTable("comentarios");

                                    b2.WithOwner()
                                        .HasForeignKey("InformacionComentarioComentarioId");
                                });

                            b1.OwnsOne("Domain.Comentarios.ValueObjects.Tag", "Tag", b2 =>
                                {
                                    b2.Property<Guid>("InformacionComentarioComentarioId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("tag");

                                    b2.HasKey("InformacionComentarioComentarioId");

                                    b2.ToTable("comentarios");

                                    b2.WithOwner()
                                        .HasForeignKey("InformacionComentarioComentarioId");
                                });

                            b1.OwnsOne("Domain.Comentarios.ValueObjects.TagUnico", "TagUnico", b2 =>
                                {
                                    b2.Property<Guid>("InformacionComentarioComentarioId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("tag_unico");

                                    b2.HasKey("InformacionComentarioComentarioId");

                                    b2.ToTable("comentarios");

                                    b2.WithOwner()
                                        .HasForeignKey("InformacionComentarioComentarioId");
                                });

                            b1.Navigation("Dados");

                            b1.Navigation("Tag")
                                .IsRequired();

                            b1.Navigation("TagUnico");
                        });

                    b.Navigation("Informacion")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Encuestas.Respuesta", b =>
                {
                    b.HasOne("Domain.Encuestas.Encuesta", null)
                        .WithMany("Respuestas")
                        .HasForeignKey("EncuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Encuestas.Voto", b =>
                {
                    b.HasOne("Domain.Encuestas.Encuesta", null)
                        .WithMany("Votos")
                        .HasForeignKey("EncuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Encuestas.Respuesta", null)
                        .WithMany()
                        .HasForeignKey("RespuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("VotanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Hilos.Hilo", b =>
                {
                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Encuestas.Encuesta", null)
                        .WithOne()
                        .HasForeignKey("Domain.Hilos.Hilo", "EncuestaId");

                    b.HasOne("Domain.Media.MediaReference", null)
                        .WithOne()
                        .HasForeignKey("Domain.Hilos.Hilo", "PortadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Hilos.ConfiguracionDeComentarios", "Configuracion", b1 =>
                        {
                            b1.Property<Guid>("HiloId")
                                .HasColumnType("uuid");

                            b1.Property<bool>("Dados")
                                .HasColumnType("boolean")
                                .HasColumnName("dados_activados");

                            b1.Property<bool>("IdUnicoActivado")
                                .HasColumnType("boolean")
                                .HasColumnName("id_unico_activado");

                            b1.HasKey("HiloId");

                            b1.ToTable("hilos");

                            b1.WithOwner()
                                .HasForeignKey("HiloId");
                        });

                    b.Navigation("Configuracion")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Encuestas.Encuesta", b =>
                {
                    b.Navigation("Respuestas");

                    b.Navigation("Votos");
                });

            modelBuilder.Entity("Domain.Hilos.Hilo", b =>
                {
                    b.Navigation("Comentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
