using System;
using System.Collections.Generic;
using System.Text;

namespace Purchase.Domain
{
   public class PurchaseDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
