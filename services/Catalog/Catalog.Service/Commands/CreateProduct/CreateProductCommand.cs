using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Service.Commands.CreateProduct
{
 public   class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Stock { get; set; }
        public decimal Price { get; set; }
    }
}
