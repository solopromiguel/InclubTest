﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Gateway.Models.Catalog.DTOs
{
  public  class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Stock { get; set; }
        public decimal Price { get; set; }
    }
}
