using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using userManager.Domain.DTOs.Usuario;
using userManager.Domain.Interfaces;
using userManager.Domain.Models.Config;

namespace userManager.Service.Services
{
    public class ServiceAuthorization : IServiceAuthorization
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public ServiceAuthorization(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
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

        public async Task<IdentityResult> AdicionarClaimRole(UserDTO user)
        {
            var usuario = await _userManager.FindByEmailAsync(user.Email);
            var adminRole = new Claim(ClaimTypes.Role, "Administrador");

            return await _userManager.AddClaimAsync(usuario, adminRole);
        }

        public async Task<string> GerarJWT(string email)
        {
            //pega a claim para retornar junto do token
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = new ClaimsIdentity(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

    }
}
