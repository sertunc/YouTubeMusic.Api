using Microsoft.AspNetCore.Mvc;
using YouTubeMusic.Api.Business.Login;
using YouTubeMusic.Api.Business.Login.Models;

namespace YouTubeMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> logger;
        private readonly ILoginBusiness loginBusiness;

        public LoginController(ILogger<LoginController> logger, ILoginBusiness loginBusiness)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.loginBusiness = loginBusiness ?? throw new ArgumentNullException(nameof(loginBusiness));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            logger.LogDebug("Login controller with userId:{userId}", model.UserId);

            var result = await loginBusiness.Login(model);

            return StatusCode(result.StatusCode, result);
        }
    }
}
