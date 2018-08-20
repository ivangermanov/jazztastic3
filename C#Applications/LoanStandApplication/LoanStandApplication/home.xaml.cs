using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Markup;
using System.Xml;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoanStandApplication
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class home : Page
    {


        public List<Element> elementsToBeDisplayed;
        List<ToCheckOutElement> elementsToBeCheckedOut;
        Border prototype ;


        public home()
        {
            InitializeComponent();
            elementsToBeCheckedOut = new List<ToCheckOutElement>();



            elementsToBeDisplayed = GlobalFunctions.ElementsToDisplay();
            //MessageBox.Show("The elements requested " + elementsToBeDisplayed.Count);

            //MessageBox.Show("ELements count " + elementsToBeDisplayed.Count);
            //MessageBox.Show(elementsToBeDisplayed.Count.ToString());
            prototype = GlobalFunctions.DeepClone((Border)CheckOutElementList.Children[0]);
            populateSideListRight();


            CheckOutElementList.Children.Clear();
            Name.Content = SessionData.EmployeeName;
            EmployeeId.Content = SessionData.EmployeeId;


        }

       
        
        Border generateANewOne(Border temp, string title, double price, int id,int quantity,BitmapImage ms)
        {
            Border local = GlobalFunctions.DeepClone(temp);

            // use the tree search to find the items
            /*
            ((Label)((StackPanel)local.Child).Children[0]).Content = id;
            ((Label)((StackPanel)local.Child).Children[1]).Content = title;
            ((Button)((StackPanel)local.Child).Children[3]).Click += new RoutedEventHandler(addElementToCheckList);
            */
            Label l = (Label)LogicalTreeHelper.FindLogicalNode(local, "IdContainer");
            l.Content = id.ToString();

            l = (Label)LogicalTreeHelper.FindLogicalNode(local, "TitleContainer");
            l.Content = title;

            l = (Label)LogicalTreeHelper.FindLogicalNode(local, "PriceContainer");
            l.Content = price.ToString()+"€";

            Button b = (Button)LogicalTreeHelper.FindLogicalNode(local, "ImageContainer");
            //b.Background = ms;
            Image i = ((Image)LogicalTreeHelper.FindLogicalNode(b, "backgroundbutton"));
            i.Source = ms;
            if (quantity > 0)
            {
                b.Click += new RoutedEventHandler(addElementToCheckList);
            }
            else
            {

                ((StackPanel)LogicalTreeHelper.FindLogicalNode(local, "container_background")).Background = Brushes.Gray;
            }


            return local;
        }
        public void EraseCheckOutList(object sender, RoutedEventArgs e)
        {


            foreach (ToCheckOutElement elem in elementsToBeCheckedOut)
            {
                for (int i = 0; i < elementsToBeDisplayed.Count; i++)
                {
                    if (elem.savedElement.itemNo == elementsToBeDisplayed[i].itemNo)
                    {
                        elementsToBeDisplayed[i].quantity += elem.savedElement.quantity;
                    }
                }
            }
            this.elementsToBeCheckedOut = new List<ToCheckOutElement>();
            populateRightSideCheckOutList();
        }
       
        public void addElementToCheckList(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Label)((StackPanel)((Button)e.Source).Parent).Children[0]).Content);
            foreach (Element x in elementsToBeDisplayed)
            {
                if (x.itemNo == id)
                {
                    if (x.quantity > 0)
                    {
                        ItemInfo i = new ItemInfo(x, 1,overlay);
                        i.FinishedCreating += new elementCreated(AddToCheckOutList);
                        overlay.Visibility = Visibility.Visible;
                        i.Show();
                        x.quantity--;
                        populateSideListRight();
                    }
                }
            }
           
        }
        private int countElements(ToCheckOutElement t, List<ToCheckOutElement> listt)
        {
            int counter = 0;
            foreach (ToCheckOutElement m in listt)
            {
                if (t.savedElement.itemNo == m.savedElement.itemNo && m.time==t.time)
                {
                    counter++;
                }
            }
            return counter;
        }
        public void AddToCheckOutList(ToCheckOutElement t)
        {
            if (t != null)
            {
                if (t == null)
                    return;
                if (countElements(t, elementsToBeCheckedOut) == 0)
                    elementsToBeCheckedOut.Add(t);
                else
                    foreach (ToCheckOutElement _element in elementsToBeCheckedOut)
                    {
                        if (t.savedElement.itemNo == _element.savedElement.itemNo && t.time == _element.time)
                        {
                            _element.quantity++;
                        }
                    }

            }

            populateRightSideCheckOutList();
        }
        private void populateSideListRight()
        {
            Border prototype = GlobalFunctions.DeepClone(ElementDisplay);
            ElementsColumns.Children.Clear();
            DockPanel currentRow = new DockPanel();
            //MessageBox.Show(elementsToBeDisplayed.Count.ToString());
            for (int i = 0; i < elementsToBeDisplayed.Count; i++)
            {
               
                if (i % 4 == 0)
                {
                    ElementsColumns.Children.Add(currentRow);
                    currentRow = new DockPanel();
                }
               
                currentRow.Children.Add(generateANewOne(prototype, elementsToBeDisplayed[i].name, (double)elementsToBeDisplayed[i].price, elementsToBeDisplayed[i].itemNo,elementsToBeDisplayed[i].quantity, elementsToBeDisplayed[i].image));
                
            }
            if (elementsToBeDisplayed.Count % 4 != 0)
            {
                ElementsColumns.Children.Add(currentRow);

            }

        }
        void updateTotalCost(Label container,int q)
        {
            container.Content = "Total container " + q;
        }
        Border generateCheckOut(Border _local_side, ToCheckOutElement _element)
        {
            Border local = GlobalFunctions.DeepClone(_local_side);

            //use names and tree structure of the interface to access elements
            /*
            ((Label)((DockPanel)local.Child).Children[0]).Content=_element.savedElement.itemNo;
            ((Label)((DockPanel)((StackPanel)((DockPanel)local.Child).Children[1]).Children[0]).Children[0]).Content = _element.savedElement.name;
            ((Label)((DockPanel)((StackPanel)((DockPanel)local.Child).Children[1]).Children[0]).Children[1]).Content = _element.savedElement.price;
            ((Label)((StackPanel)((DockPanel)local.Child).Children[1]).Children[1]).Content = _element.quantity;
            */

            Label l = (Label)LogicalTreeHelper.FindLogicalNode(local, "idContainer");
            l.Content = _element.savedElement.itemNo;
            l = (Label)LogicalTreeHelper.FindLogicalNode(local, "NameContainer");
            l.Content = _element.savedElement.name;
            l = (Label)LogicalTreeHelper.FindLogicalNode(local, "priceContainer");
            l.Content = _element.savedElement.price;
            l = (Label)LogicalTreeHelper.FindLogicalNode(local, "quantityContainer");
            l.Content = _element.quantity;
            Button b = (Button)LogicalTreeHelper.FindLogicalNode(local, "EraserButton");
            b.Click += new RoutedEventHandler(eraseCheckOutElement);
            return local;

        }
        private void eraseCheckOutElement(object sender,RoutedEventArgs e)
        {

            Button b=(Button)e.Source;

            DockPanel tmp=(DockPanel)b.Parent;


            Label l = (Label)LogicalTreeHelper.FindLogicalNode(tmp, "idContainer");
            //MessageBox.Show(l.Content.ToString());
            int idToErase = Convert.ToInt32(l.Content);

            for (int i = 0; i < elementsToBeCheckedOut.Count; i++)
            {
                if (elementsToBeCheckedOut[i].savedElement.itemNo == idToErase)
                {
                    foreach (Element elem in elementsToBeDisplayed)
                    {
                        if (elem.itemNo == elementsToBeCheckedOut[i].savedElement.itemNo)
                        {
                            elem.quantity++;
                            
                        }
                    }
                    if (elementsToBeCheckedOut[i].quantity > 1)
                    {
                        elementsToBeCheckedOut[i].quantity--;

                    }
                    else
                    {
                        elementsToBeCheckedOut.RemoveAt(i);

                    }
                    
                    break;

                }
            }
            populateRightSideCheckOutList();
            populateSideListRight();


        }
        void populateRightSideCheckOutList()
        {
            int total = 0;
            //MessageBox.Show(elementsToBeCheckedOut.Count.ToString());
            CheckOutElementList.Children.Clear();
            foreach (ToCheckOutElement t in elementsToBeCheckedOut)
            {
                CheckOutElementList.Children.Add(generateCheckOut(prototype,t));
                total += t.quantity * (int)t.savedElement.price * t.time;
            }
            updateTotalCost(totalCostLabel, total);
        }
        void restartPage() 
        {
            SessionData.CreatePages();
            NavigationService.Navigate(SessionData.LoanShopPage);
        }



        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
                SessionData.CreatePages();
                NavigationService.Navigate(SessionData.DashboardPage);
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (elementsToBeCheckedOut.Count > 0)
            {
                Checkout page = new Checkout(elementsToBeCheckedOut, calculateTotal(elementsToBeCheckedOut), SessionData.latestReceipt,overlay);
                page.recalloutside += new Checkout.finishedWithCheckingOut(restartPage);
                SessionData.latestReceipt = GlobalFunctions.returnReceiptLastNr();
                overlay.Visibility = Visibility.Visible;
                page.Show();
                
            }
            else
            {
                MessageBox.Show("Error , you cannot check out without anything in the list.");
            }
        }
        private int calculateTotal( List<ToCheckOutElement> local)
        {
            int total = 0;
            foreach (ToCheckOutElement x in local)
            {
                total += (int)x.savedElement.price * x.quantity * x.time;
            }
            return total;
        }
        private void logout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }
    }
}
