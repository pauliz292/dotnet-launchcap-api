using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Property;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class PropertyController : BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<PropertyDto>> ListAllProperties()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{propertyId}")]
        public async Task<ActionResult<Unit>> Update(int propertyId, Update.Command command)
        {
            command.Id = propertyId;
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