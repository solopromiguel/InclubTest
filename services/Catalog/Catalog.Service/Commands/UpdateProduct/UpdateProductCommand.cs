using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Service.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
