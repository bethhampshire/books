using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAppUI.Models
{
    public class ValueDataType
    {
        public decimal Offer { get; set; }

        public string Title { get; set; }
    }

    public class ZiffitModel
    {
        public ValueDataType Value { get; set; }

        public bool Success { get; set; }
    }
}
