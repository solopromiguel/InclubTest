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

namespace Catalog.Service.Queries.GetProductAll
{
   public class GetProductAllQuery : IRequest<IEnumerable<ProductBriefDto>>
    {
        public class GetProductAllHandler : IRequestHandler<GetProductAllQuery, IEnumerable<ProductBriefDto>>
        {
            private readonly IConfiguration _configuration;
            public GetProductAllHandler(IConfiguration configuration)
            {
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            }
            public async Task<IEnumerable<ProductBriefDto>> Handle(GetProductAllQuery query, CancellationToken cancellationToken)
            {
                using (var connection = new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
                {
                    string sql = "SELECT * FROM dbo.products";

                    var products = await connection.QueryAsync<ProductBriefDto>(sql).ConfigureAwait(false);
                    return products;
                }
            }
         }
    }

}
