using System;
using System.Collections.Generic;
using Domain.Hilos;
using Domain.Hilos.ValueObjects;
using Domain.Usuarios;
using Domain.Features.Medias.Models.ValueObjects;
using Domain.Categorias;
using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Encuestas;
using Domain.Notificaciones;
using Domain.Stickies;
using SharedKernel;
using Xunit;
using SharedKernel.Abstractions;
using Domain.Hilos.DomainEvents;

namespace Domain.Tests.Features.Hilos.Models
{
    public class HiloTest
    {
        [Fact]
        public void Eliminar_HiloYaEliminado_ReturnsYaEliminado()
        {
            // Arrange
            var hilo = CreateHilo();
            hilo.Eliminar();

            // Act
            var result = hilo.Eliminar();

            // Assert
            Assert.Equal(HilosFailures.YaEliminado, result);
        }

        [Fact]
        public void Eliminar_HiloConSticky_EliminaSticky()
        {
            // Arrange
            var hilo = CreateHilo();
            hilo.EstablecerSticky();

            // Act
            hilo.Eliminar();

            // Assert
            Assert.Null(hilo.Sticky);
        }

        [Fact]
        public void Eliminar_HiloConDenuncias_DesestimaDenuncias()
        {
            // Arrange
            var hilo = CreateHilo();
            var usuarioId = new UsuarioId(Guid.NewGuid());
            hilo.Denunciar(usuarioId);

            // Act
            hilo.Eliminar();

            // Assert
            foreach (var denuncia in hilo.Denuncias)
            {
                Assert.True(denuncia.Desestimada);
            }
        }

        [Fact]
        public void Eliminar_Hilo_ChangesStatusToEliminado()
        {
            // Arrange
            var hilo = CreateHilo();

            // Act
            hilo.Eliminar();

            // Assert
            Assert.Equal(HiloStatus.Eliminado, hilo.Status);
        }

        [Fact]
        public void Eliminar_Hilo_RaisesHiloEliminadoDomainEvent()
        {
            // Arrange
            var hilo = CreateHilo();
            var domainEvents = new List<IDomainEvent>();

            // Act
            hilo.Eliminar();

            // Assert
            Assert.Contains(domainEvents, e => e is HiloEliminadoDomainEvent);
        }

        private Hilo CreateHilo()
        {
            var titulo = Titulo.Create("Test Titulo").Value;
            var descripcion = Descripcion.Create("Test Descripcion").Value;
            var autor = new Autor("Anonimo", "Anon");
            var autorId = new UsuarioId(Guid.NewGuid());
            var portada = new MediaSpoileableId(Guid.NewGuid());
            var subcategoria = new SubcategoriaId(Guid.NewGuid());
            var encuesta = new EncuestaId(Guid.NewGuid());
            var configuracion = new ConfiguracionDeComentarios(true, true);

            return Hilo.Create(titulo, descripcion, autor, autorId, portada, subcategoria, encuesta, configuracion);
        }
    }
}