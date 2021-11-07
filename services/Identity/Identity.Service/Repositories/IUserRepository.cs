using Identity.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Service.Repositories
{
   public interface IUserRepository
    {
        User Register(User vm);
    }
}
