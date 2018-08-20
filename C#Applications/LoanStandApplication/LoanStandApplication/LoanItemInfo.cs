using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LoanStandApplication {
    class LoanItemInfo : StackPanel {
        public int VisitorNo { get; private set; }
        public int ItemNo { get; private set; }
        public int StandNo { get; private set; }
        public string Purchased { get; private set; }
        public string Returned { get; private set; }
        public int Quantity { get; private set; }

        public int OriginalQuantity { get; private set; }
        public event ReturnItemHandler ItemReturn;

        public LoanItemInfo(int visitorNo, MySqlDataReader reader) {
            //Set properties of LoanItemInfo
            VisitorNo = visitorNo;
            ItemNo = Convert.ToInt32(reader[0]);
            StandNo = Convert.ToInt32(reader[1]);
            Purchased = Convert.ToDateTime(reader[2]).ToString("yyyy-MM-dd HH:mm:ss");
            Returned = Convert.ToDateTime(reader[3]).ToString("yyyy-MM-dd HH:mm:ss");
            Quantity = Convert.ToInt32(reader[4]);
            OriginalQuantity = Quantity;

            Width = 500;
            Height = 130;
            Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#AB538D"));
            Orientation = Orientation.Horizontal;
            Margin = new Thickness(0, 0, 0, 20);

            //Creating Detail Stack
            StackPanel detailStack = new StackPanel {
                Width = 300,
                Height = 130,
                Orientation = Orientation.Horizontal,
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF78A2BB"))
            };

            //Creating Label Detail Stack
            StackPanel labelsDetailStack = new StackPanel {
                Width = 150,
                Height = 130
            };
            string[] labels = { "ItemNo", "StandNo", "Purchased", "Return", "Quantity" };

            for (int j = 0; j < labels.Length; j++) {
                Label label = new Label {
                    Content = labels[j]
                };
                labelsDetailStack.Children.Add(label);
            }

            //Creating Info Detail Stack
            StackPanel infosDetailStack = new StackPanel {

                Width = 150,
                Height = 130
            };

            for (int j = 0; j < labels.Length; j++) {
                Label label = new Label {
                    Content = reader[j].ToString()
                };
                if (j == 0) {
                    label.Content += $", {reader[5]}";
                }
                infosDetailStack.Children.Add(label);
            }

            //Creating Return Button
            Button button = new Button {
                Content = "Return",
                FontSize = 24,
                Width = 200,
                Height = 130,
                Foreground = Brushes.White,
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#283043"))
            };

            detailStack.Children.Add(labelsDetailStack);
            detailStack.Children.Add(infosDetailStack);
            Children.Add(detailStack);
            Children.Add(button);
        }

        public void DecreaseQuantity() {
            if(Quantity > 0) {
                Quantity--;
                ((Label)((StackPanel)((StackPanel)Children[0]).Children[1]).Children[4]).Content = Quantity;
                if(Quantity == 0) {
                    Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C8C8C8"));
                }
            }
            
        }

        public void UpdateLoanItemInfo() {
            ItemReturn.Invoke(ItemNo, StandNo, Purchased, Returned, Quantity);
        }
    }
}
