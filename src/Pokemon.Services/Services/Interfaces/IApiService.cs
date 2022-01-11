using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Services.Services.Interfaces
{
    public interface IApiService
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
