﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20240817003118_initial_p")]
    partial class initial_p
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Baneos.Baneo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Concluye")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("concluye");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("mensaje");

                    b.Property<Guid>("ModeradorId")
                        .HasColumnType("uuid")
                        .HasColumnName("moderador_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid>("UsuarioBaneadoId")
                        .HasColumnType("uuid")
                        .HasColumnName("usuario_baneado_id");

                    b.HasKey("Id");

                    b.HasIndex("ModeradorId");

                    b.HasIndex("UsuarioBaneadoId");

                    b.ToTable("baneos", (string)null);
                });

            modelBuilder.Entity("Domain.Categorias.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nombre");

                    b.Property<bool>("OcultoPorDefecto")
                        .HasColumnType("boolean")
                        .HasColumnName("oculto_por_defecto");

                    b.HasKey("Id");

                    b.ToTable("categorias", (string)null);
                });

            modelBuilder.Entity("Domain.Categorias.Subcategoria", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("Categoria")
                        .HasColumnType("uuid")
                        .HasColumnName("categoria_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nombre");

                    b.Property<string>("NombreCorto")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nombre_corto");

                    b.HasKey("Id");

                    b.HasIndex("Categoria");

                    b.ToTable("subcategorias", (string)null);
                });

            modelBuilder.Entity("Domain.Comentarios.Comentario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AutorId")
                        .HasColumnType("uuid")
                        .HasColumnName("autor_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Destacado")
                        .HasColumnType("boolean")
                        .HasColumnName("destacado");

                    b.Property<Guid>("Hilo")
                        .HasColumnType("uuid")
                        .HasColumnName("hilo_id");

                    b.Property<bool>("RecibirNotificaciones")
                        .HasColumnType("boolean")
                        .HasColumnName("recibir_notificaciones");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.ComplexProperty<Dictionary<string, object>>("Texto", "Domain.Comentarios.Comentario.Texto#Texto", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("encuesta_id");
                        });

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("Hilo");

                    b.ToTable("comentarios", (string)null);
                });

            modelBuilder.Entity("Domain.Comentarios.DenunciaDeComentario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ComentarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("comentarios_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DenuncianteId")
                        .HasColumnType("uuid")
                        .HasColumnName("denunciante_id");

                    b.Property<int>("Razon")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("ComentarioId");

                    b.HasIndex("DenuncianteId");

                    b.ToTable("denuncias_de_comentario", (string)null);
                });

            modelBuilder.Entity("Domain.Comentarios.RelacionDeComentario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ComentarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("comentario_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Oculto")
                        .HasColumnType("boolean")
                        .HasColumnName("oculto");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id");

                    b.HasIndex("ComentarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("relaciones_de_comentarios", (string)null);
                });

            modelBuilder.Entity("Domain.Encuestas.Encuesta", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("encuestas", (string)null);
                });

            modelBuilder.Entity("Domain.Hilos.DenunciaDeHilo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DenuncianteId")
                        .HasColumnType("uuid")
                        .HasColumnName("denunciante_id");

                    b.Property<Guid>("HiloId")
                        .HasColumnType("uuid")
                        .HasColumnName("hilo_id");

                    b.Property<int>("Razon")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("DenuncianteId");

                    b.HasIndex("HiloId");

                    b.ToTable("denuncias_de_hilo", (string)null);
                });

            modelBuilder.Entity("Domain.Hilos.Hilo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AutorId")
                        .HasColumnType("uuid")
                        .HasColumnName("autor_id");

                    b.Property<Guid>("Categoria")
                        .HasColumnType("uuid")
                        .HasColumnName("subcategoria_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Encuesta")
                        .HasColumnType("uuid")
                        .HasColumnName("encuesta_id");

                    b.Property<bool>("RecibirNotificaciones")
                        .HasColumnType("boolean")
                        .HasColumnName("recibir_notificaciones");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("UltimoBump")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ultimo_bump");

                    b.ComplexProperty<Dictionary<string, object>>("Descripcion", "Domain.Hilos.Hilo.Descripcion#Descripcion", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("descripcion");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Titulo", "Domain.Hilos.Hilo.Titulo#Titulo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("titulo");
                        });

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("Categoria");

                    b.HasIndex("Encuesta")
                        .IsUnique();

                    b.ToTable("hilos", (string)null);
                });

            modelBuilder.Entity("Domain.Hilos.RelacionDeHilo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Favorito")
                        .HasColumnType("boolean")
                        .HasColumnName("favorito");

                    b.Property<Guid>("HiloId")
                        .HasColumnType("uuid")
                        .HasColumnName("hilo_id");

                    b.Property<bool>("Oculto")
                        .HasColumnType("boolean")
                        .HasColumnName("oculto");

                    b.Property<bool>("Seguido")
                        .HasColumnType("boolean")
                        .HasColumnName("seguido");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id");

                    b.HasIndex("HiloId")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("relaciones_de_hilo", (string)null);
                });

            modelBuilder.Entity("Domain.Stickies.Sticky", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("Conluye")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("concluye_at");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Hilo")
                        .HasColumnType("uuid")
                        .HasColumnName("hilo_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("Hilo")
                        .IsUnique();

                    b.ToTable("stickies", (string)null);
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

            modelBuilder.Entity("Domain.Baneos.Baneo", b =>
                {
                    b.HasOne("Domain.Usuarios.Moderador", null)
                        .WithMany()
                        .HasForeignKey("ModeradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuarios.Anonimo", null)
                        .WithMany()
                        .HasForeignKey("UsuarioBaneadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Categorias.Subcategoria", b =>
                {
                    b.HasOne("Domain.Categorias.Categoria", null)
                        .WithMany()
                        .HasForeignKey("Categoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Comentarios.Comentario", b =>
                {
                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Hilos.Hilo", null)
                        .WithMany()
                        .HasForeignKey("Hilo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Comentarios.DenunciaDeComentario", b =>
                {
                    b.HasOne("Domain.Comentarios.Comentario", null)
                        .WithMany()
                        .HasForeignKey("ComentarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("DenuncianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Comentarios.RelacionDeComentario", b =>
                {
                    b.HasOne("Domain.Comentarios.Comentario", null)
                        .WithMany()
                        .HasForeignKey("ComentarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Encuestas.Encuesta", b =>
                {
                    b.OwnsMany("Domain.Encuestas.Respuesta", "Respuestas", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Contenido")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("contenido");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("EncuestaId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("EncuestaId");

                            b1.ToTable("respuestas", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("EncuestaId");
                        });

                    b.OwnsMany("Domain.Encuestas.Voto", "Votos", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("EncuestaId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("RespuestaId")
                                .HasColumnType("uuid")
                                .HasColumnName("respuesta_id");

                            b1.Property<Guid>("VotanteId")
                                .HasColumnType("uuid")
                                .HasColumnName("votante_id");

                            b1.HasKey("Id");

                            b1.HasIndex("EncuestaId");

                            b1.HasIndex("VotanteId");

                            b1.ToTable("votos", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("EncuestaId");

                            b1.HasOne("Domain.Usuarios.Usuario", null)
                                .WithMany()
                                .HasForeignKey("VotanteId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.Navigation("Respuestas");

                    b.Navigation("Votos");
                });

            modelBuilder.Entity("Domain.Hilos.DenunciaDeHilo", b =>
                {
                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("DenuncianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Hilos.Hilo", null)
                        .WithMany()
                        .HasForeignKey("HiloId")
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

                    b.HasOne("Domain.Categorias.Subcategoria", null)
                        .WithMany()
                        .HasForeignKey("Categoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Encuestas.Encuesta", null)
                        .WithOne()
                        .HasForeignKey("Domain.Hilos.Hilo", "Encuesta");

                    b.OwnsOne("Domain.Hilos.ValueObjects.ConfiguracionDeComentarios", "Configuracion", b1 =>
                        {
                            b1.Property<Guid>("HiloId")
                                .HasColumnType("uuid");

                            b1.Property<bool>("Dados")
                                .HasColumnType("boolean")
                                .HasColumnName("dados");

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

            modelBuilder.Entity("Domain.Hilos.RelacionDeHilo", b =>
                {
                    b.HasOne("Domain.Hilos.Hilo", null)
                        .WithOne()
                        .HasForeignKey("Domain.Hilos.RelacionDeHilo", "HiloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Stickies.Sticky", b =>
                {
                    b.HasOne("Domain.Hilos.Hilo", null)
                        .WithOne()
                        .HasForeignKey("Domain.Stickies.Sticky", "Hilo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
