using Catalog.Service.Commands.CreateProduct;
using Catalog.Service.Commands.DeleteProduct;
using Catalog.Service.Commands.UpdateProduct;
using Catalog.Service.Queries.GetProductAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductBriefDto>> GetAll()
        {
            return await _mediator.Send(new GetProductAllQuery {});
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}
