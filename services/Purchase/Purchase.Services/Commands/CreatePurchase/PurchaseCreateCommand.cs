using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Purchase.Service.Commands.CreatePurchase
{
   public class PurchaseCreateCommand : INotification
    {
        //public int ClientId { get; set; }
        public int UserId { get; set; }
        public IEnumerable<PurchaseCreateDetail> Items { get; set; } = new List<PurchaseCreateDetail>();
    }

    public class PurchaseCreateDetail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
