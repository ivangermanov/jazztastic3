using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoanStandApplication
{
    public class Element
    {
        public Element(int no, typeElement tp, float p, string n, string d, string l,int q,BitmapImage ms)
        {
            itemNo = no;
            typeOfTheElement = tp;
            price = p;
            name = n;
            description = d;
            LinkToImage = l;
            quantity = q;
            this.image = ms;
        }
        
        public BitmapImage image { get; set; }
        public int quantity { get; set; }
        public string LinkToImage { get; set; }
        public int itemNo { get; set; }
        public typeElement typeOfTheElement { get; set; }
        public double price { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public override string ToString()
        {
            return " " + itemNo + " " + typeOfTheElement + " " + price + " " + name + " " + description;
        }

    }
    public enum typeElement
    {
        BUY, LOAN
    }
}

