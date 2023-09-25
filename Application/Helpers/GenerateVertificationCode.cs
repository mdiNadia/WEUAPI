using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class GenerateVertificationCode
    {
        public static string CreateVertificationCode()
        {
            Random R = new Random();

            double NUD_1Value = 1;
            double NUD_2Value = 999999999999999; //15-digit number

            var next = R.NextDouble();

            double v = NUD_1Value + (next * (NUD_2Value - NUD_1Value));

            return v.ToString().Substring(0, 4);
        }
    }
}
