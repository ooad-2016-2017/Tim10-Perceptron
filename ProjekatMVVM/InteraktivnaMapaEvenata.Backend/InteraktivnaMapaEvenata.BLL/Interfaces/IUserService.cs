﻿using InteraktivnaMapaEvenata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraktivnaMapaEvenata.BLL.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetUser(string id);
    }
}
