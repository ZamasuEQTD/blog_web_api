using SharedKernel;

namespace Domain.Encuestas;

public static class EncuestasFailures
{
    public static readonly Error NoEncontrado = new Error("Encuesta.NoEncontrado", "La encuesta no fue encontrada");
}