using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Markup;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Net;

using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace LoanStandApplication
{
    class GlobalFunctions
    {
        private static typeElement stringToTypeOfElement(string a)
        {
            switch (a.ToLower())
            {
                case "loan":
                    return typeElement.LOAN;
                    
            }
            return typeElement.BUY;
        }

        public static BitmapImage getImageToStream(string link)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(link);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = stream;
            bi.EndInit();
            /*
            ImageBrush ib = new ImageBrush(bi);
            return ib;
            */
            return bi;
        }




        public static List<Element> ElementsToDisplay()
        {
            List<Element> temporary = new List<Element>();
            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                //MessageBox.Show(SessionData.Elements());
                using (MySqlCommand command = new MySqlCommand(SessionData.Elements(), connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                //MessageBox.Show(reader[4].ToString());
                                temporary.Add(new Element(Convert.ToInt32(reader[0].ToString()), stringToTypeOfElement(reader[1].ToString()), (float)Convert.ToDouble(reader[2]), reader[3].ToString(), reader[5].ToString(), reader[4].ToString(), Convert.ToInt32(reader[11]), getImageToStream(reader[4].ToString())));

                            }
                        }
                        else
                        {
                            MessageBox.Show("Error MySqlDataReader was null!");
                        }
                    }
                }
                connection.Close();
            }
            return temporary;
        }
        public static Border DeepClone<Border>(Border element)
        {
            var xaml = XamlWriter.Save(element);

            var xamlString = new StringReader(xaml);

            var xmlTextReader = new XmlTextReader(xamlString);

            var deepCopyObject = (Border)XamlReader.Load(xmlTextReader);

            return deepCopyObject;
        }
        public static DockPanel DeepCloneDock<DockPanel>(DockPanel element)
        {
            var xaml = XamlWriter.Save(element);

            var xamlString = new StringReader(xaml);

            var xmlTextReader = new XmlTextReader(xamlString);

            var deepCopyObject = (DockPanel)XamlReader.Load(xmlTextReader);

            return deepCopyObject;
        }
        public static StackPanel DeepCloneStack<StackPanel>(StackPanel element)
        {
            var xaml = XamlWriter.Save(element);

            var xamlString = new StringReader(xaml);

            var xmlTextReader = new XmlTextReader(xamlString);

            var deepCopyObject = (StackPanel)XamlReader.Load(xmlTextReader);

            return deepCopyObject;
        }
        public static int returnReceiptLastNr()
        {

            using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SessionData.getTheLatestReceipt(), connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            reader.Read();
                            if (!reader.IsDBNull(0))
                                return Convert.ToInt32(reader[0]) + 1;
                            else
                               return 0;
                            //return Convert.ToInt32(reader[0].ToString())+1;
                        }
                        else
                        {
                            MessageBox.Show("Error MySqlDataReader was null!");
                        }
                    }
                }
                connection.Close();
            }
            return -1;
        }
    }
}
