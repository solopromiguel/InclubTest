using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Purchase.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Purchase.Service.Commands.CreatePurchase
{
  public  class PurchaseCreateEventHandler :
        INotificationHandler<PurchaseCreateCommand>
    {
        private readonly IConfiguration _configuration;
        public PurchaseCreateEventHandler(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task Handle(PurchaseCreateCommand notification, CancellationToken cancellationToken)
        {
            //"--- New order creation started";
            var entry = new Domain.Purchase();

            using (var connection = new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                //Begin the transaction

                using (var transaction = connection.BeginTransaction())
                {
                    // 01. Prepare detail
                    PrepareDetail(entry, notification);

                    // 02. Prepare header
                    PrepareHeader(entry, notification);

                    // 03. Create order

                    //Create and fill-up master table data
                    var paramMaster = new DynamicParameters();
                    //paramMaster.Add("@ClientId", entry.ClientId, DbType.AnsiString);
                    paramMaster.Add("@UserId", entry.UserId, DbType.AnsiString);
                    paramMaster.Add("@CreatedAt", entry.CreatedAt, DbType.AnsiString);
                    paramMaster.Add("@Total", entry.Total, DbType.AnsiString);

                    //Insert record in master table. Pass transaction parameter to Dapper.
                    var affectedRows = connection.Execute("insert into purchases (ClientId, UserId , CreatedAt, Total) VALUES( @ClientId, @UserId , @CreatedAt, @Total)",
                                                           paramMaster, transaction: transaction);

                    //Get the Id newly created for master table record.
                    var newId = Convert.ToInt64(connection.ExecuteScalar<object>("SELECT @@IDENTITY", null, transaction: transaction));


                    // 04. Create order details

                    //Create and fill-up detail table data
                    foreach (var item in entry.Items)
                    {
                        var paramDetails = new DynamicParameters();
                        paramDetails.Add("@OrderMasterId", newId);


                        //Insert record in detail table. Pass transaction parameter to Dapper.
                        var affectedRowsDetail = connection.Execute("insert into purchaseDetails....", paramDetails, transaction: transaction);
                    }


                    //Commit transaction
                    transaction.Commit();
                }
            }

            //"--- New order creation ended";
        }

        private void PrepareDetail(Domain.Purchase entry, PurchaseCreateCommand notification)
        {
            entry.Items = notification.Items.Select(x => new PurchaseDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.Price,
                Total = x.Price * x.Quantity
            }).ToList();
        }

        private void PrepareHeader(Domain.Purchase entry, PurchaseCreateCommand notification)
        {
            // Header information
            entry.UserId = notification.UserId;
            //entry.ClientId = notification.ClientId;
            entry.CreatedAt = DateTime.UtcNow;

            // Sum
            entry.Total = entry.Items.Sum(x => x.Total);
        }

    }
}
