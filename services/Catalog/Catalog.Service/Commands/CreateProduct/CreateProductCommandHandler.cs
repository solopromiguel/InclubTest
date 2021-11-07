using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.Commands.CreateProduct
{
  public  class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IConfiguration _configuration;
        public CreateProductCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                string sql = @"INSERT INTO dbo.products (name, description, stock , price)
                                VALUES (@name, @description, @stock , @price);
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                var entityId = connection.Query<int>(sql, request).Single();
                return entityId;
            }
        }
      }
}
