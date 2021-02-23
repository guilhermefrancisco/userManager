using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using userManager.Domain.DTOs.Usuario;
using userManager.Domain.Interfaces;

namespace userManager.Service.Services
{
    public class ServiceAuthorization : IServiceAuthorization
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ServiceAuthorization(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<SignInResult> Entrar(LoginUserDTO loginUser)
        {
            return await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Senha, false, true);
        }

        public async Task<IdentityResult> Registrar(RegisterUserDTO registerUser)
        {
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            return await _userManager.CreateAsync(user, registerUser.Senha);
        }

        public async Task<string> GerarJWT(string email)
        {
            return "falta gerar token";
        }

    }
}
