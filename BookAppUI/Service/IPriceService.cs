using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAppUI.Models;

namespace BookAppUI.Service
{
    public interface IPriceService
    {
        Task<PriceModel> GetPrice(StoreType storeType);
    }
}
