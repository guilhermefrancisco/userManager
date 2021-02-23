using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using userManager.Domain.DTOs.Usuario;
using userManager.Domain.Interfaces;

namespace userManager.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IServiceAuthorization _serviceAuthorization;
        public AuthorizationController(IServiceAuthorization serviceAuthorization) => _serviceAuthorization = serviceAuthorization;

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _serviceAuthorization.Entrar(loginUser);

            if (result.Succeeded)
            {
                //gerar token e retornar
                return Ok(new
                {
                    success = true,
                    data = "token"
                });
            }

            return BadRequest("Usuário ou senha inválidos");
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Cadastrar(RegisterUserDTO registerUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _serviceAuthorization.Registrar(registerUser);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var x = await _serviceAuthorization.Entrar(new LoginUserDTO() { Email = registerUser.Email, Senha = registerUser.Senha });

            //gerar token e retornar
            return Ok(new
            {
                success = true,
                data = "token"
            });
        }

    }
}
