using Domain.Categorias;
using Domain.Denuncias.Rules;
using Domain.Hilos;
using Domain.Hilos.Rules;
using Domain.Usuarios;
using Domain.Usuarios.Rules;
using FluentAssertions;
using NUnit.Framework;
using Tests.Seeds;

namespace Tests.Domain.Hilos
{

    public class HiloBaseTest : TestBase
    {
        internal DateTime UtcNow => DateTime.UtcNow;
        internal readonly Usuario Autor;
        internal readonly List<Usuario> Seguidores;
        internal readonly Usuario Seguidor;
        internal readonly Usuario Denunciante;

        public HiloBaseTest()
        {
            Autor = new Anonimo(Username.Create("CODUBI2024"),"PASSWORD");

            Denunciante = new Anonimo(Username.Create("GATUBI2024"),"PASSWORD");

            Seguidor = new Anonimo(Username.Create("GATUBI2024"),"PASSWORD");

            Seguidores = [];

            for (int i = 0; i < 12; i++) {
                Seguidores.Add(
                    new Anonimo(Username.Create("GATUBI2024"),"PASSWORD")
                );
            }

        }
    }
    [TestFixture]
    public class HiloTest : HiloBaseTest
    {
        private readonly Hilo Hilo;
        public HiloTest()
        {
            Hilo = new Hilo(
                "titulo",
                "descripcion",
                new SubcategoriaId(Guid.NewGuid()),
                Autor,
                null,
                new (
                    false,
                    false
                )
            );
        }

        [Test]
        public void Denunciar_Rompe_HiloDebeEstarActivo_Cuando_HiloEstaEliminado(){
            Hilo.Eliminar(DateTime.UtcNow);

            AssertBrokenRule<HiloDebeEstarActivoRule>(() => {
                Hilo.Denunciar(
                Seguidor.Id
            );
            });
            
        }

        [Test]
        public void Denunciar_Rompe_SoloPuedeDenunciarUnaVezRule_Cuando_UsuarioYaHaDenunciado(){
            Hilo.Denunciar(
                Seguidor.Id
            );

            AssertBrokenRule<SoloPuedeDenunciarUnaVezRule>(() => {
                Hilo.Denunciar(
                Seguidor.Id
                );
            });
            
        }

        [Test]
        public void Denunciar_Agrega_Denuncia_CuandoEsValido(){
            Hilo.Denunciar(
                Denunciante.Id
            ); 

            bool HaDenunciado = Hilo.Denuncias.Any(d=> d.DenuncianteId == Denunciante.Id);

            HaDenunciado.Should().BeTrue();
        }

        [Test]
        public void EstablecerSticky_Rompe_HiloDebeEstarActivoRule_CuandoHiloNoEstaActivo(){
            Hilo.Eliminar(
                DateTime.UtcNow
            ); 

            AssertBrokenRule<HiloDebeEstarActivoRule>(() => {
                Hilo.EstablecerSticky(
                    UtcNow.AddMinutes(100),
                    UtcNow
                );
            });
        }

        [Test]
        public void EstablecerSticky_Rompe_NoDebeTenerStickyActivo_CuandoHiloTieneStickyValido(){
            Hilo.EstablecerSticky(
                UtcNow.AddMinutes(100),
                UtcNow
            );
            
            AssertBrokenRule<NoDebeTenerStickyActivoRule>(() => {
                Hilo.EstablecerSticky(
                    UtcNow.AddMinutes(100),
                    UtcNow
                );
            });
        }

        [Test]
        public void EstablecerSticky_AgregaSticky_CuandoEsValido(){
            Hilo.EstablecerSticky(
                UtcNow.AddMinutes(100),
                UtcNow
            );
            
            Hilo.TieneStickyActivo(UtcNow).Should().BeTrue();
        }

        [Test]
        public void EliminarSticky_Rompe_DebeTenerStickyActivoRule(){
            AssertBrokenRule<DebeTenerStickyActivoRule>(() => {
                Hilo.EliminarSticky(
                    UtcNow
                );
            });
        }
        [Test]
        public void EliminarSticky_DebeEliminarStickyActivo(){
            Hilo.EstablecerSticky(
                UtcNow.AddMinutes(100),
                UtcNow
            );

            Hilo.EliminarSticky(
                UtcNow
            );

            Hilo.TieneStickyActivo(UtcNow).Should().BeFalse();
        }
    }
}