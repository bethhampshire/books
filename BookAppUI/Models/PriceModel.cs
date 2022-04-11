using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAppUI.Models
{
    public enum StoreType
    {
        WeBuyBooks,
        MusicMagpie,
        Ziifiit
    }
    public class PriceModel
    {
        // barcode
        // book title
        // price
        public decimal Price { get; set; }
        public string Message { get; set; }
        public string Album { get; set; }
        public StatusEnum Status { get; set; }
    }
}
