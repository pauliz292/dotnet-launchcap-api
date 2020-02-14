using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoryController : BaseController
    {
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> ListAllCategories()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{categoryId}")]
        public async Task<CategoryDto> GetProduct(int categoryId)
        {
            return await Mediator.Send(new GetCategory.Query { Id = categoryId });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult<Unit>> Update(int categoryId, Update.Command command)
        {
            command.Id = categoryId;
            return await Mediator.Send(command);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<Unit>> Delete(int categoryId, Delete.Command command)
        {
            command.Id = categoryId;
            return await Mediator.Send(command);
        }
    }
}