using Dapper;
using Identity.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Identity.Service.Repositories
{
   public class UserRepository: IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public User Register(User vm)
        {
            using (var connection = new SqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {

                string sql = @"INSERT INTO dbo.users (FirstName, LastName ,UserName , Email, PasswordHash , PasswordSalt)
                                VALUES (@FirstName, @LastName, @UserName , @Email, @PasswordHash, @PasswordSalt);
                                SELECT CAST(SCOPE_IDENTITY() as int)";

                vm.Id = connection.Query<int>(sql, vm).Single();
                return vm;
            }
        }
    }
}
