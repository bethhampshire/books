using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAppUI.Models
{
    public class WeBuyBooksModel
    {
        public Item Item { get; set; }
        public Basket Basket { get; set; }
        public Logs Logs { get; set; }
        public decimal Price { get; set; }
        public string StatusCode { get; set; }
        public StatusEnum Status { get; set; }
        public string Error { get; set; }

    }

    public class Item
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }

    public class Basket
    {
    }

    public class Logs
    {
    }
}
