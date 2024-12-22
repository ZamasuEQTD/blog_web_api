using Application.Abstractions;
using Domain.Usuarios;
using Microsoft.AspNetCore.Identity;

namespace Infraestructure.Authentication;

public class RolesProvider : IRolesProvider
{
    private readonly UserManager<Usuario> userManager;
    public RolesProvider(UserManager<Usuario> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<IEnumerable<string>> GetRoles(Usuario usuario)
    {
       return await userManager.GetRolesAsync(usuario);
    }
}