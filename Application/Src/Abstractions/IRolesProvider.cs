using Domain.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Application.Abstractions
{
    public interface IRolesProvider
    {
        public Task<IEnumerable<string>> GetRoles(Usuario usuario);
    }
}