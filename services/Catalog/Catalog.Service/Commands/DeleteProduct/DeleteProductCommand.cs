using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.Commands.DeleteProduct
{
  public  class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IConfiguration _configuration;
        public DeleteProductCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync("DELETE FROM products WHERE Id = @Id",
                new { Id = request.Id });

                if (affected == 0)
                    return false;

                return true;
            }
          }

     }
  }
