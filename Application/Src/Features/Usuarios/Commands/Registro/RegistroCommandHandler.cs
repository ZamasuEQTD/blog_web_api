using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.ValueObjects;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Application.Usuarios.Commands
{
    public class RegistroCommandHandler : ICommandHandler<RegistroCommand, string>
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public RegistroCommandHandler(IUsuariosRepository usuariosRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IPasswordHasher passwordHasher, UserManager<Usuario> userManager, RoleManager<Role> roleManager)
        {
            _usuariosRepository = usuariosRepository;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<string>> Handle(RegistroCommand request, CancellationToken cancellationToken)
        {
            Result<Username> username = Username.Create(request.Username);

            if(username.IsFailure) return username.Error;

            Result<Password> password = Password.Create(request.Password);

            if(password.IsFailure) return password.Error;
 
            Usuario? usuario = await  _userManager.FindByNameAsync(request.Username);

            if (usuario is not null) return UsuariosFailures.UsernameOcupado;

            usuario = new Usuario {
                Id = new UsuarioId(Guid.NewGuid()),
                UserName = request.Username,
                PasswordHash = _passwordHasher.Hash(password.Value)
            };

            var result = await _userManager.CreateAsync(usuario);

            if(!result.Succeeded) throw new Exception("Error al crear el usuario");

            string role = Role.Anonimo.Name!;

            if (!await _roleManager.RoleExistsAsync(role)) await _roleManager.CreateAsync(Role.Anonimo);

            await _userManager.AddToRoleAsync(usuario, role);


            return await _jwtProvider.Generar(usuario);
        }
    }
}