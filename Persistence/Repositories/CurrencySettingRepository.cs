﻿using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
{
    public class CurrencySettingRepository : GenericRepository<CurrencySetting>, ICurrencySettingRepository
    {

        public CurrencySettingRepository(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }
    }
}
