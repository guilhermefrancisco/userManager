using AutoMapper;
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
        private readonly IMapper _mapper;
        public AuthorizationController(IServiceAuthorization serviceAuthorization, IMapper mapper)
        {
            _serviceAuthorization = serviceAuthorization;
            _mapper = mapper;
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _serviceAuthorization.Entrar(loginUser);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    success = true,
                    data = _serviceAuthorization.GerarJWT(loginUser.Email)
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

            var loginUser = _mapper.Map<RegisterUserDTO, LoginUserDTO>(registerUser);

            await _serviceAuthorization.Entrar(loginUser);

            return Ok(new
            {
                success = true,
                data = _serviceAuthorization.GerarJWT(loginUser.Email)
            });
        }

    }
}
