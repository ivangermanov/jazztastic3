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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShoppingStand
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    /// 
  
    public partial class home : Page
    {
        

        List<Element> lst;
        public home()
        {
            InitializeComponent();
            initialize_elements();
            Name.Content = SessionData.EmployeeName;
            EmployeeId.Content = SessionData.EmployeeId;
            Logic.CheckOutList = new List<Element>();

        }
        private void initialize_elements()
        {
            GlobalDefinitions.checkoutElement = Logic.DeepClone((Border)CheckOutElementList.Children[0]);
            CheckOutElementList.Children.Clear();
            lst = Logic.RetrieveElementsList();
            createElements();
        }









        private DockPanel generateRowForElements(int i)
        {
            DockPanel pane=new DockPanel();
            pane.Name = "row" + i;
            return pane;
        }
        Border generateANewOne(Border temp, string title, double price, int id, int quantity,BitmapImage ms)
        {
            Border local = Logic.DeepClone(temp);

            // use the tree search to find the items
            /*
            ((Label)((StackPanel)local.Child).Children[0]).Content = id;
            ((Label)((StackPanel)local.Child).Children[1]).Content = title;
            ((Button)((StackPanel)local.Child).Children[3]).Click += new RoutedEventHandler(addElementToCheckList);
            */
            Label l = (Label)LogicalTreeHelper.FindLogicalNode(local, "id");
            l.Content = id.ToString();

            l = (Label)LogicalTreeHelper.FindLogicalNode(local, "TitleContainer");
            l.Content = title;

            l = (Label)LogicalTreeHelper.FindLogicalNode(local, "PriceContainer");
            l.Content = price.ToString() + "€";

            Button b = (Button)LogicalTreeHelper.FindLogicalNode(local, "ImageContainer");
            Image i = ((Image)LogicalTreeHelper.FindLogicalNode(b, "backgroundbutton"));
            i.Source = ms;
            if (quantity > 0)
            {
                b.Click += new RoutedEventHandler(AddElemenToCheckOut);
            }
            else
            {

                ((StackPanel)LogicalTreeHelper.FindLogicalNode(local, "container_background")).Background = Brushes.Gray;
            }


            return local;
        }

        public void createElements()
        {
            Border prototype = Logic.DeepClone(ElementDisplay);
            ElementsColumns.Children.Clear();
            DockPanel currentRow = new DockPanel();
            //MessageBox.Show(elementsToBeDisplayed.Count.ToString());
            for (int i = 0; i < lst.Count; i++)
            {
                
                if (i % 4 == 0)
                {
                    ElementsColumns.Children.Add(currentRow);
                    currentRow = new DockPanel();
                }
                currentRow.Children.Add(generateANewOne(prototype, lst[i].name, (double)lst[i].price, lst[i].itemNo, lst[i].qoh,lst[i].image));
                

            }
            if (lst.Count % 4 != 0)
            {
                ElementsColumns.Children.Add(currentRow);

            }
            /*
            Border prototype,current;
            UIElement prototypeRow,placeHolder;
            int elementsToAdd,limit;

            prototype = Logic.DeepClone(ElementDisplay);
            elementsToAdd = lst.Count;
            limit = 4;
            ElementsColumns.Children.RemoveAt(0);



            for (int k = 0; elementsToAdd>0;)
            {
                prototypeRow = generateRowForElements(k++);

                if (elementsToAdd < 4)
                {
                    limit = elementsToAdd;
                }

                for (int i = 0; i < limit; i++)
                {
                    
                    current = Logic.DeepClone(prototype);
                    placeHolder = current.Child;
                    ((Label)((StackPanel)placeHolder).Children[1]).Content = lst[elementsToAdd-1].name;
                    if (lst[elementsToAdd - 1].qoh > 0)
                        ((Button)((StackPanel)placeHolder).Children[2]).Click += new RoutedEventHandler(AddElemenToCheckOut);
                    else
                        ((StackPanel)placeHolder).Background = Brushes.Gray;
                    ((Label)((StackPanel)placeHolder).Children[0]).Content = lst[elementsToAdd - 1].itemNo;
                    ((DockPanel)prototypeRow).Children.Add(current);
                    elementsToAdd--;
                }
                ElementsColumns.Children.Add(prototypeRow);
                
            }
            */


        }
        private int countElementsById(Element a, List<Element> b)
        {
            int k = 0;
            foreach (Element x in b)
            {
                if (a.name == x.name)
                {
                    k++;
                }

            }
            return k;
        }
        private void UpdateTotalCost(Label t,List<Element> listCheck)
        {
            double cost = 0;
            foreach (Element x in listCheck)
            {
                cost += x.price;
            }
            t.Content = "Total cost: "+cost+" €";
        }
        private void updateDisplayListCheck()
        {
            Border element;
            Border current;
          




            element = GlobalDefinitions.checkoutElement;
            CheckOutElementList.Children.Clear();




            for (int i = 0; i < Logic.CheckOutList.Count; i++)
            {
                if (countElementsById(Logic.CheckOutList[i],Logic.CheckOutList.GetRange(0,i))==0)
                {

                    current = Logic.DeepClone(element);

                    //current_ = (StackPanel)((DockPanel)current.Child).Children[1];


                    Label l = (Label)LogicalTreeHelper.FindLogicalNode(current, "NameContainer");
                    l.Content = Logic.CheckOutList[i].name;
                    //((Label)((DockPanel)current_.Children[0]).Children[0]).Content = Logic.CheckOutList[i].name;

                    l = (Label)LogicalTreeHelper.FindLogicalNode(current, "priceContainer");
                    l.Content = "Price:"+Logic.CheckOutList[i].price+" €";
                    // ((Label)((DockPanel)current_.Children[0]).Children[1]).Content = Logic.CheckOutList[i].price;


                    l = (Label)LogicalTreeHelper.FindLogicalNode(current, "quantityContainer");
                    l.Content="Quantity"+countElementsById(Logic.CheckOutList[i], Logic.CheckOutList);
                    //((Label)current_.Children[1]).Content = countElementsById(Logic.CheckOutList[i], Logic.CheckOutList);


                    l = (Label)LogicalTreeHelper.FindLogicalNode(current, "idContainer");
                    l.Content = Logic.CheckOutList[i].itemNo;
                    //((Label)((DockPanel)current.Child).Children[0]).Content = Logic.CheckOutList[i].itemNo;

                    ((Button)LogicalTreeHelper.FindLogicalNode(current, "EraserButton")).Click+=Button_Click_1;
                    

                    CheckOutElementList.Children.Add(current);
                }
            }


            UpdateTotalCost(totalCostLabel,Logic.CheckOutList);

        }

        private void AddElemenToCheckOut(object sender, RoutedEventArgs e)
        {
            
            int a = Convert.ToInt32(((Label)((StackPanel)((Button)e.Source).Parent).Children[0]).Content);
            foreach (Element x in lst)
            {
                if (x.itemNo == a)
                {
                    Logic.addCheckOutList(x);
                    x.qoh--;
                }
            }
            updateDisplayListCheck();
            createElements();


        }
        private void reloadPage()
        {
            SessionData.CreateANewElementsPage();

            NavigationService.Navigate(SessionData.ElementsPage);
        }
        private int calculateTotal(List<Element> lst)
        {
            int total = 0;
            foreach (Element e in lst)
            {
                total += (int)e.price;
            }
            return total;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Logic.CheckOutList.Count > 0)
            {
               
                Checkout t = new Checkout(calculateTotal(Logic.CheckOutList),lst);
                t.receiptFinished += new Checkout.reloadElementsPage(reloadPage);
                overlay.Visibility = Visibility.Visible;
                t.Show();
            }
            else
            {
                MessageBox.Show("Error:Not enough elements in the checkout list!", "Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
          
        }

        public void logout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           int id = Convert.ToInt32(((Label)((DockPanel)((Button)e.Source).Parent).Children[0]).Content);
          
            for (int i = 0; i < Logic.CheckOutList.Count; i++)
            {
                if (Logic.CheckOutList[i].itemNo == id)
                {
                   
                    Logic.CheckOutList.RemoveAt(i);
                    foreach (Element t in lst)
                    {
                        if (t.itemNo == id)
                        {
                            t.qoh++;
                        }
                    }
                    break;
                }
            }
            updateDisplayListCheck();

            createElements();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(SessionData.DashboardPage);
        }
        private void EraseCheckOutList(object sender, RoutedEventArgs e)
        {
            foreach (Element t in Logic.CheckOutList)
            {
                foreach (Element x in lst)
                {
                    if (t.itemNo == x.itemNo)
                    {
                        x.qoh++;
                    }
                }
            }
            Logic.CheckOutList = new List<Element>();
            updateDisplayListCheck();
            createElements();
        }

        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
