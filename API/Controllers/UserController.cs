using System.Collections.Generic;
using System.Threading.Tasks;
using Application.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<UserDto>> CurrentUser()
        {
            return await Mediator.Send(new CurrentUser.Query());
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<Unit>> ChangePassword(ChangePassword.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}