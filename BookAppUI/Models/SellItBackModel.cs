using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAppUI.Models
{
    public class SellItBackModel
    {
        public decimal Price{ get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public StatusEnum Status { get; set; }

    }
}
