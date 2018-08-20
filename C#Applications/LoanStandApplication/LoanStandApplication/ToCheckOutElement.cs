using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanStandApplication
{
    public class ToCheckOutElement
    {
        public Element savedElement { get; set; }
        public int time { get; set; }
        public int quantity { get; set; }

        public ToCheckOutElement(Element t, int quant, int time)
        {
            savedElement = t;
            quantity = quant;
            this.time = time;
        }
    }
}
