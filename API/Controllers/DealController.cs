using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Deal;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class DealController : BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<DealDto>> ListAllDeals()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        // [HttpPut("{borrowerId}")]
        // public async Task<ActionResult<Unit>> Update(int borrowerId, Update.Command command)
        // {
        //     command.Id = borrowerId;
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