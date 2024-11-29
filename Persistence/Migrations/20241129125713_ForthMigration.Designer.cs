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
    [Migration("20241129125713_ForthMigration")]
    partial class ForthMigration
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

                    b.Property<DateTime?>("Concluye")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("concluye");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Mensaje")
                        .HasColumnType("text")
                        .HasColumnName("mensaje");

                    b.Property<Guid>("ModeradorId")
                        .HasColumnType("uuid")
                        .HasColumnName("moderador_id");

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

                    b.Property<int?>("Dados")
                        .HasColumnType("integer")
                        .HasColumnName("dados");

                    b.Property<Guid>("Hilo")
                        .HasColumnType("uuid")
                        .HasColumnName("hilo_id");

                    b.Property<Guid?>("HiloId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("MediaReferenceId")
                        .HasColumnType("uuid")
                        .HasColumnName("media");

                    b.Property<bool>("RecibirNotificaciones")
                        .HasColumnType("boolean")
                        .HasColumnName("recibir_notificaciones");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tag");

                    b.Property<string>("TagUnico")
                        .HasColumnType("text")
                        .HasColumnName("tag_unico");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("texto");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("Hilo");

                    b.HasIndex("HiloId");

                    b.HasIndex("MediaReferenceId");

                    b.ToTable("comentarios", (string)null);
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

            modelBuilder.Entity("Domain.Hilos.Hilo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AutorId")
                        .HasColumnType("uuid")
                        .HasColumnName("usuario_id");

                    b.Property<Guid>("Categoria")
                        .HasColumnType("uuid")
                        .HasColumnName("subcategoria_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("Encuesta")
                        .HasColumnType("uuid")
                        .HasColumnName("encuesta_id");

                    b.Property<Guid>("PortadaId")
                        .HasColumnType("uuid")
                        .HasColumnName("portada_id");

                    b.Property<bool>("RecibirNotificaciones")
                        .HasColumnType("boolean")
                        .HasColumnName("recibir_notificaciones");

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

                    b.HasIndex("PortadaId");

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

                    b.HasIndex("HiloId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("relaciones_de_hilo", (string)null);
                });

            modelBuilder.Entity("Domain.Media.HashedMedia", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hash");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.Property<string>("tipo_de_archivo")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.HasKey("Id");

                    b.HasIndex("Hash")
                        .IsUnique();

                    b.ToTable("media", (string)null);

                    b.HasDiscriminator<string>("tipo_de_archivo").HasValue("HashedMedia");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Media.MediaReference", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MediaId")
                        .HasColumnType("uuid")
                        .HasColumnName("media_id");

                    b.Property<bool>("Spoiler")
                        .HasColumnType("boolean")
                        .HasColumnName("spoiler");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.ToTable("media_references", (string)null);
                });

            modelBuilder.Entity("Domain.Notificaciones.Notificacion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("NotificadoId")
                        .HasColumnType("uuid")
                        .HasColumnName("notificado_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("tipo_de_notificacion")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.HasKey("Id");

                    b.HasIndex("NotificadoId");

                    b.ToTable("notificaciones", (string)null);

                    b.HasDiscriminator<string>("tipo_de_notificacion").HasValue("Notificacion");

                    b.UseTphMappingStrategy();
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

            modelBuilder.Entity("Domain.Media.FileMedia", b =>
                {
                    b.HasBaseType("Domain.Media.HashedMedia");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("filename");

                    b.HasDiscriminator().HasValue("FileMedia");
                });

            modelBuilder.Entity("Domain.Media.NetworkMedia", b =>
                {
                    b.HasBaseType("Domain.Media.HashedMedia");

                    b.HasDiscriminator().HasValue("NetworkMedia");
                });

            modelBuilder.Entity("Domain.Notificaciones.HiloComentadoNotificacion", b =>
                {
                    b.HasBaseType("Domain.Notificaciones.Notificacion");

                    b.Property<Guid>("ComentarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("comentario_id");

                    b.Property<Guid>("HiloId")
                        .HasColumnType("uuid")
                        .HasColumnName("hilo_id");

                    b.HasIndex("ComentarioId");

                    b.HasIndex("HiloId");

                    b.HasDiscriminator().HasValue("hilo_comentado");
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

            modelBuilder.Entity("Domain.Media.Imagen", b =>
                {
                    b.HasBaseType("Domain.Media.FileMedia");

                    b.HasDiscriminator().HasValue("imagen");
                });

            modelBuilder.Entity("Domain.Media.Video", b =>
                {
                    b.HasBaseType("Domain.Media.FileMedia");

                    b.HasDiscriminator().HasValue("video");
                });

            modelBuilder.Entity("Domain.Media.YoutubeVideo", b =>
                {
                    b.HasBaseType("Domain.Media.NetworkMedia");

                    b.HasDiscriminator().HasValue("youtube");
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

                    b.HasOne("Domain.Hilos.Hilo", null)
                        .WithMany("Comentarios")
                        .HasForeignKey("HiloId");

                    b.HasOne("Domain.Media.MediaReference", null)
                        .WithMany()
                        .HasForeignKey("MediaReferenceId");

                    b.OwnsMany("Domain.Comentarios.DenunciaDeComentario", "Denuncias", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<Guid>("ComentarioId")
                                .HasColumnType("uuid")
                                .HasColumnName("comentario_id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("DenuncianteId")
                                .HasColumnType("uuid")
                                .HasColumnName("denunciante_id");

                            b1.Property<int>("Razon")
                                .HasColumnType("integer");

                            b1.Property<int>("Status")
                                .HasColumnType("integer")
                                .HasColumnName("status");

                            b1.HasKey("Id");

                            b1.HasIndex("ComentarioId");

                            b1.HasIndex("DenuncianteId");

                            b1.ToTable("denuncias_de_comentario", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ComentarioId");

                            b1.HasOne("Domain.Usuarios.Usuario", null)
                                .WithMany()
                                .HasForeignKey("DenuncianteId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.OwnsMany("Domain.Comentarios.RelacionDeComentario", "Relaciones", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<Guid>("ComentarioId")
                                .HasColumnType("uuid")
                                .HasColumnName("comentario_id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<bool>("Oculto")
                                .HasColumnType("boolean")
                                .HasColumnName("oculto");

                            b1.Property<Guid>("UsuarioId")
                                .HasColumnType("uuid")
                                .HasColumnName("usuario_id");

                            b1.HasKey("Id");

                            b1.HasIndex("ComentarioId");

                            b1.HasIndex("UsuarioId");

                            b1.ToTable("relaciones_de_comentario", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ComentarioId");

                            b1.HasOne("Domain.Usuarios.Usuario", null)
                                .WithMany()
                                .HasForeignKey("UsuarioId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.OwnsOne("Domain.Comentarios.ValueObjects.Colores", "Color", b1 =>
                        {
                            b1.Property<Guid>("ComentarioId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("color");

                            b1.HasKey("ComentarioId");

                            b1.ToTable("comentarios");

                            b1.WithOwner()
                                .HasForeignKey("ComentarioId");
                        });

                    b.OwnsOne("Domain.Comentarios.ValueObjects.ComentarioStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("ComentarioId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("status");

                            b1.HasKey("ComentarioId");

                            b1.ToTable("comentarios");

                            b1.WithOwner()
                                .HasForeignKey("ComentarioId");
                        });

                    b.OwnsMany("Domain.Comentarios.Respuesta", "Respuestas", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("RespondidoId")
                                .HasColumnType("uuid")
                                .HasColumnName("comentario_respondido_id");

                            b1.Property<Guid>("RespuestaId")
                                .HasColumnType("uuid")
                                .HasColumnName("respuesta_id");

                            b1.HasKey("Id");

                            b1.HasIndex("RespondidoId");

                            b1.HasIndex("RespuestaId");

                            b1.ToTable("respuestas_comentarios", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RespondidoId");

                            b1.HasOne("Domain.Comentarios.Comentario", null)
                                .WithMany()
                                .HasForeignKey("RespuestaId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.Navigation("Color")
                        .IsRequired();

                    b.Navigation("Denuncias");

                    b.Navigation("Relaciones");

                    b.Navigation("Respuestas");

                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Encuestas.Encuesta", b =>
                {
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

                            b1.Property<Guid?>("encuesta_id")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("encuesta_id");

                            b1.ToTable("respuestas", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("encuesta_id");
                        });

                    b.Navigation("Respuestas");

                    b.Navigation("Votos");
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

                    b.HasOne("Domain.Media.MediaReference", null)
                        .WithMany()
                        .HasForeignKey("PortadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Stickies.Sticky", "Sticky", b1 =>
                        {
                            b1.Property<Guid>("Hilo")
                                .HasColumnType("uuid")
                                .HasColumnName("hilo_id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid?>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("Hilo");

                            b1.ToTable("stickies", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Hilo");
                        });

                    b.OwnsMany("Domain.Hilos.ComentarioDestacado", "ComentarioDestacados", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<Guid>("ComentarioId")
                                .HasColumnType("uuid")
                                .HasColumnName("comentario_id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("created_at");

                            b1.Property<Guid>("HiloId")
                                .HasColumnType("uuid")
                                .HasColumnName("hilo_id");

                            b1.HasKey("Id");

                            b1.HasIndex("ComentarioId");

                            b1.HasIndex("HiloId");

                            b1.ToTable("comentarios_destacados", (string)null);

                            b1.HasOne("Domain.Comentarios.Comentario", null)
                                .WithMany()
                                .HasForeignKey("ComentarioId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("HiloId");
                        });

                    b.OwnsMany("Domain.Hilos.DenunciaDeHilo", "Denuncias", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("DenuncianteId")
                                .HasColumnType("uuid")
                                .HasColumnName("denunciante_id");

                            b1.Property<Guid>("HiloId")
                                .HasColumnType("uuid")
                                .HasColumnName("hilo_id");

                            b1.Property<int>("Razon")
                                .HasColumnType("integer")
                                .HasColumnName("razon");

                            b1.Property<int>("Status")
                                .HasColumnType("integer")
                                .HasColumnName("status");

                            b1.HasKey("Id");

                            b1.HasIndex("DenuncianteId");

                            b1.HasIndex("HiloId");

                            b1.ToTable("denuncias_de_hilo", (string)null);

                            b1.HasOne("Domain.Usuarios.Usuario", null)
                                .WithMany()
                                .HasForeignKey("DenuncianteId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("HiloId");
                        });

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

                    b.OwnsOne("Domain.Hilos.ValueObjects.HiloStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("HiloId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("status");

                            b1.HasKey("HiloId");

                            b1.ToTable("hilos");

                            b1.WithOwner()
                                .HasForeignKey("HiloId");
                        });

                    b.Navigation("ComentarioDestacados");

                    b.Navigation("Configuracion")
                        .IsRequired();

                    b.Navigation("Denuncias");

                    b.Navigation("Status")
                        .IsRequired();

                    b.Navigation("Sticky");
                });

            modelBuilder.Entity("Domain.Hilos.RelacionDeHilo", b =>
                {
                    b.HasOne("Domain.Hilos.Hilo", null)
                        .WithMany()
                        .HasForeignKey("HiloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Media.MediaReference", b =>
                {
                    b.HasOne("Domain.Media.HashedMedia", null)
                        .WithMany()
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Notificaciones.Notificacion", b =>
                {
                    b.HasOne("Domain.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("NotificadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Media.FileMedia", b =>
                {
                    b.OwnsOne("Domain.Media.ValueObjects.MediaSource", "Source", b1 =>
                        {
                            b1.Property<Guid>("FileMediaId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("media_source");

                            b1.HasKey("FileMediaId");

                            b1.ToTable("media");

                            b1.WithOwner()
                                .HasForeignKey("FileMediaId");
                        });

                    b.Navigation("Source")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Media.NetworkMedia", b =>
                {
                    b.OwnsOne("Domain.Media.ValueObjects.NetworkSource", "Source", b1 =>
                        {
                            b1.Property<Guid>("NetworkMediaId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("network_source");

                            b1.HasKey("NetworkMediaId");

                            b1.ToTable("media");

                            b1.WithOwner()
                                .HasForeignKey("NetworkMediaId");
                        });

                    b.Navigation("Source")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Notificaciones.HiloComentadoNotificacion", b =>
                {
                    b.HasOne("Domain.Comentarios.Comentario", null)
                        .WithMany()
                        .HasForeignKey("ComentarioId")
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
                    b.Navigation("Comentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
