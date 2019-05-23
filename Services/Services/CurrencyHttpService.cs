using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CurrencyHttpService : ICurrencyHttpService
    {
        public async Task<double> GetEuroToUSdRate() {
            //Assume we do a http request to goverment api to get rate 
            return 1.4;
        }

       
    }
}
