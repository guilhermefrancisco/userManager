using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using userManager.Domain.DTOs.Usuario;

namespace userManager.Domain.Interfaces
{
    public interface IServiceAuthorization
    {
        Task<SignInResult> Entrar(LoginUserDTO loginUser);
        Task<IdentityResult> Registrar(RegisterUserDTO registerUser);
        Task<string> GerarJWT(string email);
    }
}
