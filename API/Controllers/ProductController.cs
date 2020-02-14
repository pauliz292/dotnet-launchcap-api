using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> ListAllProducts()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{productId}")]
        public async Task<ProductDto> GetProduct(int productId)
        {
            return await Mediator.Send(new GetProduct.Query { Id = productId });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult<Unit>> Update(int productId, Update.Command command)
        {
            command.Id = productId;
            return await Mediator.Send(command);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<Unit>> Delete(int productId, Delete.Command command)
        {
            command.Id = productId;
            return await Mediator.Send(command);
        }
    }
}