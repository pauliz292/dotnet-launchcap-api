using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Borrower;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class BorrowerController : BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<BorrowerDto>> ListAllBorrowers()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        // [HttpPut("{productId}")]
        // public async Task<ActionResult<Unit>> Update(int productId, Update.Command command)
        // {
        //     command.Id = productId;
        //     return await Mediator.Send(command);
        // }

        // [HttpDelete("{productId}")]
        // public async Task<ActionResult<Unit>> Delete(int productId, Delete.Command command)
        // {
        //     command.Id = productId;
        //     return await Mediator.Send(command);
        // }
    }
}