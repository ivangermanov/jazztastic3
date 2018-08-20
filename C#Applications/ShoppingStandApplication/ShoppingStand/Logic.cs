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
using System.Windows.Media.Imaging;
using System.Net;

namespace ShoppingStand
{
    class Logic
    {
        public static List<Element> CheckOutList;

        public static void addCheckOutList(Element temp)
        {

            for (int i = 0; i < CheckOutList.Count; i++)
            {
                if (CheckOutList[i].itemNo == temp.itemNo)
                {
                    CheckOutList.Insert(i, temp);
                    return;
                }
            }
            CheckOutList.Add(temp);
        }
        public void RemoveElement(Element tmp)
        {
            for (int i = 0; i < CheckOutList.Count; i++)
            {
                if (CheckOutList[i].name == tmp.name)
                {
                    CheckOutList.RemoveAt(i);
                }
            }
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
        public static List<Element> RetrieveElementsList()
        {
            List<Element> tmp = new List<Element>();
                using (MySqlConnection connection = new MySqlConnection(SessionData.connectionInfo))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(SessionData.Elements(), connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                while (reader.Read())
                                    {
                                    //MessageBox.Show(reader[1].ToString());
                                    tmp.Add(new Element(
                                    Convert.ToInt32(reader[0]),

                                    typeElement.BUY,
                                    (float)Convert.ToDouble(reader[2]),
                                    Convert.ToString(reader[3]),
                                    Convert.ToString(reader[4]),
                                    Convert.ToString(reader[5]),
                                    Convert.ToInt32(reader[11]),
                                    getImageToStream(reader[4].ToString())
                                        )
                                     );
                                    

                                }
                            }
                            else
                            {
                                throw new Exception("The reader for MYSQL not working.");
                            }
                        }
                    }
                }
            return tmp;
            }
            

        
        }
    }

