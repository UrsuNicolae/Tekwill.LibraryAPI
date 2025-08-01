using Google.Apis.Auth;
using Library.Aplication.Interfaces;
using Library.Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthApi.Controllers;

[ApiController]
[Route("v1/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IGoogleService _googleService;
    private readonly GoogleConfigurations _googleConfigurations;

    public AuthController(IGoogleService googleService,
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IOptions<GoogleConfigurations> googleConfigurations)
    {
        _googleService = googleService;
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _googleConfigurations = googleConfigurations.Value;
    }

    [HttpGet]
    public async Task<IActionResult> SignIn()
    {
        var url = _googleService.GetRedirectLink();
        return Redirect(url);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallBack(string code, CancellationToken cancellationToken)
    {
        GoogleJsonWebSignature.Payload payload;
        var idToken = await _googleService.GetIdToken(code);
        payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _googleConfigurations.ClientId }
        });

        var user = await _userRepository.GetByEmail(payload.Email);
        if(user == null)
        {
            user = new Library.Domain.Entities.User
            {
                Email = payload.Email
            };
            await _userRepository.CreateUser(user);
        }
        var token = _jwtTokenGenerator.GenerateToken(user);
        return Ok(token);
    }
}
