﻿using FishingStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingStoreApp.DataAccess.Repository.IRepository
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
        void Update(Promotion obj);
    }
}
