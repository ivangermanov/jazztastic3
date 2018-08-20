using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoanStandApplication
{
    public delegate void elementCreated(ToCheckOutElement el);
    /// <summary>
    /// Interaction logic for ItemInfo.xaml
    /// </summary>
    public partial class ItemInfo : Window
    { 
        private Element local_element;
        private int local_quantity;
        private Rectangle outer_reference;
        public event elementCreated FinishedCreating;
        int countDay = 1;

        public ItemInfo(Element x,int quantity,Rectangle rec)
        {
            outer_reference = rec;
            local_element = x;
            local_quantity = quantity;
            InitializeComponent();
            updateInfo(countDay);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FinishedCreating(null);
            outer_reference.Visibility = Visibility.Hidden;
            this.Close();
            
        }
        public void hide_overlay(object sender,CancelEventArgs c)
        {
            outer_reference.Visibility = Visibility.Hidden;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FinishedCreating(new ToCheckOutElement(local_element,1,countDay));
            
            this.Close();
        }
        private void updateInfo(int x)
        {
            ItemReturnDateC.Content = DateTime.Today.AddDays(x).ToString("d");
            ItemRentCostC.Content = (countDay)*local_element.price;
            ItemNameC.Content = local_element.name;
            ItemNoC.Content = local_element.itemNo;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            if (countDay > 1)
            {
                countDay--;
            }
            updateInfo(countDay);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (countDay < 3)
            {
                countDay++;
            }
            updateInfo(countDay);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
