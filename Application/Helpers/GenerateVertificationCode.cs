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
            var code = Guid.NewGuid().ToString("N").Substring(0, 7);
            return code;
        }
    }
}
