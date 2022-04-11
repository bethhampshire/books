using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAppUI.Models
{
    public enum StatusEnum
    {
        ItemAccepted = 0,
        ItemNotFound = 1,
        DuplicateItem = 2,
        ItemNotAccepted = 3,
        InvalidBarcode = 4,
        Unauthenticated = 5
    }
}
