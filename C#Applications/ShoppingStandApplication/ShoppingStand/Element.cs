using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ShoppingStand
{
    public class Element
    {
        public Element(int no, typeElement tp, float p, string n, string d, string l,int q,BitmapImage b)
        {
            itemNo = no;
            typeOfTheElement = tp;
            price = p;
            name = n;
            description = d;
            LinkToImage = l;
            qoh = q;
            this.image = b;
        }
        public BitmapImage image { get; set; }

        public string LinkToImage { get; set; }
        public int itemNo { get; set; }
        public typeElement typeOfTheElement {get; set; }
        public double price { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int qoh { get; set; }
        public override string ToString()
        {
            return " " + itemNo + " " + typeOfTheElement + " " + price + " " + name + " " + description;
    }

    }
    public enum typeElement
    {
        BUY,LOAN
    }
    
}
