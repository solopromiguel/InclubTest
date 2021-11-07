using Catalog.Domain;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.Commands.UpdateProduct
{
   public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand , bool>
    {
        private readonly IConfiguration _configuration;
        public UpdateProductCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync
                    ("UPDATE products SET Name=@ProductName, Description = @Description, Price = @Price WHERE Id = @Id",
                            new { ProductName = request.Name, Description = request.Description, Price = request.Price, Id = request.Id });


                if (affected == 0)
                    return false;

                return true;

            }
        }
      }
}
