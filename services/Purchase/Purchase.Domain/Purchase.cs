using System;
using System.Collections.Generic;
using System.Text;

namespace Purchase.Domain
{
   public class Purchase
    {
        public int Id { get; set; }
        //public int ClientId { get; set; }
        public int UserId { get; set; }
        public ICollection<PurchaseDetail> Items { get; set; } = new List<PurchaseDetail>();
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
    }
}
