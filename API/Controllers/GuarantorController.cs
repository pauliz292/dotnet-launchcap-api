using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Guarantor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class GuarantorController : BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<GuarantorDto>> ListAllGuarantors()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{guarantorId}")]
        public async Task<ActionResult<Unit>> Update(int guarantorId, Update.Command command)
        {
            command.Id = guarantorId;
            return await Mediator.Send(command);
        }

        // [HttpDelete("{productId}")]
        // public async Task<ActionResult<Unit>> Delete(int productId, Delete.Command command)
        // {
        //     command.Id = productId;
        //     return await Mediator.Send(command);
        // }
    }
}