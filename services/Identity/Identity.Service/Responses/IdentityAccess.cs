﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Service.Responses
{
    public class IdentityAccess
    {
        public bool Succeeded { get; set; }
        public string AccessToken { get; set; }
    }
}
