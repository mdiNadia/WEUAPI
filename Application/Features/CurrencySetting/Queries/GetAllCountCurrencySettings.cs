﻿using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.CurrencySetting.Queries
{
    public class GetAllCountCurrencySettings : IRequest<int>
    {
        public class GetAllCountCurrencySettingsHandler : IRequestHandler<GetAllCountCurrencySettings, int>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCountCurrencySettingsHandler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }
            public async Task<int> Handle(GetAllCountCurrencySettings query, CancellationToken cancellationToken)
            {
                try
                {
                    return await _unitOfWork.CurrencySettings.GetQueryList().AsNoTracking().CountAsync();

                }
                catch (Exception)
                {

                    throw new RestException(HttpStatusCode.InternalServerError, "خطایی رخ داد، متن خطا را به پشتیبان ارجاع دهید!");

                }
            }
        }
    }
}
