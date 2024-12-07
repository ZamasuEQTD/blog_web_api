using System.Data;
using Application.Comentarios.GetComentarioDeHilos;

namespace Application.Abstractions.Data
{
    public interface IDBConnectionFactory
    {
        IDbConnection CreateConnection();
    }

}