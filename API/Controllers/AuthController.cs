using System.Threading.Tasks;
using Application.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("register-user")]
        public async Task<ActionResult<UserDto>> Register(Register.Command command)
        {
            command.Role = Application.Static.Roles.User;
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("register-clinic-personnel")]
        public async Task<ActionResult<UserDto>> RegisterAdmin(Register.Command command)
        {
            command.Role = Application.Static.Roles.Admin;
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("confirm-email")]
        public async Task<ActionResult<UserDto>> ConfirmEmail(ConfirmEmail.Command command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ActionResult<Unit>> ForgotPassword(ForgotPassword.Command command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult<Unit>> ResetPassword(ResetPassword.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}