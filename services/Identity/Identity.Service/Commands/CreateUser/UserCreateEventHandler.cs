using Identity.Domain;
using Identity.Service.Common.Extensions;
using Identity.Service.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.Commands.CreateUser
{
    public class UserCreateEventHandler :
       IRequestHandler<UserCreateCommand, int>
    {
        private readonly IUserRepository _userRepository;
        public UserCreateEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> Handle(UserCreateCommand notification, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;

            var entry = new User
            {
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                Email = notification.Email,
                UserName = notification.Email
            };

            if (!String.IsNullOrEmpty(notification.Password))
            {

                SecurityExtension.CreatePasswordHash(notification.Password, out passwordHash, out passwordSalt);
                entry.PasswordHash = passwordHash;
                entry.PasswordSalt = passwordSalt;
            }

            var result = _userRepository.Register(entry);

            return result.Id;
        }
    }
}
