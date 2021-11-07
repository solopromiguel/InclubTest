using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Service.Queries.GetProductAll
{
    public   class ProductBriefDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Stock { get; set; }
        public decimal Price { get; set; }
    }
}
