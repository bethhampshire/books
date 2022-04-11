using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAppUI.Models
{
    public enum StatusEnum
    {
        ItemNotFound = 0,
        DuplicateItem = 1,
        ItemNotAccepted = 2,
        InvalidBarcode = 3,
        Unauthenticated = 4,
        ItemAccepted = 5
    }
}
