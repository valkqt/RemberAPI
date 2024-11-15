﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface ICardsRepository
    {
        public Task<IEnumerable<Card>> ListAsync();
    }
}
