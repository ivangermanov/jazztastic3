using System;
using System.Collections.Generic;
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
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace ManagementApplication.Charts {
    /// <summary>
    /// Interaction logic for VisitorCheckedInChart.xaml
    /// </summary>
    public partial class VisitorCheckedInChart : Window {
        public VisitorCheckedInChart() {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Day 1",
                    Values = new ChartValues<int> { 0, 0, 0, 0 }
                }
            };
            
            SeriesCollection.Add(new ColumnSeries {
                Title = "Day 2",
                Values = new ChartValues<int> { 0, 0, 0, 0 }
            });

            SeriesCollection.Add(new ColumnSeries {
                Title = "Day 3",
                Values = new ChartValues<int> { 0, 0, 0, 0 }
            });

            Labels = new[] { "09:00", "12:00", "15:00", "18:00" };
            Formatter = value => value.ToString("N");

            DataContext = this;
            LoadChartData();
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void LoadChartData() {
            try {
                using (MySqlConnection connection = new MySqlConnection(SessionData.ConnectionInfo)) {
                    connection.Open();
                    for(int i = 26; i < 29; i++) {
                        for(int j = 0; j < 4; j++) {
                            using (MySqlCommand command = new MySqlCommand(SessionData.EventOverviewGetVisitorChartData(GetStartDate(i, j), GetEndDate(i, j)), connection)) {
                                using(MySqlDataReader reader = command.ExecuteReader()) {
                                    reader.Read();
                                    if(reader.HasRows) {
                                        SeriesCollection[i-26].Values[j] = Convert.ToInt32(reader[0]);
                                    }
                                }
                            }
                        }
                    }
                }
            } catch(Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        private string GetStartDate(int day, int timeFrame) {
            string timeFrameStr = "";
            switch(timeFrame) {
                case 0: timeFrameStr = "09:00:00";
                    break;
                case 1:
                    timeFrameStr = "12:00:00";
                    break;
                case 2:
                    timeFrameStr = "15:00:00";
                    break;
                case 3:
                    timeFrameStr = "18:00:00";
                    break;
            }
            return $"2018-06-{day} {timeFrameStr}";
        }
        private string GetEndDate(int day, int timeFrame) {
            string timeFrameStr = "";
            switch (timeFrame) {
                case 0:
                    timeFrameStr = "11:59:59";
                    break;
                case 1:
                    timeFrameStr = "14:59:59";
                    break;
                case 2:
                    timeFrameStr = "17:59:59";
                    break;
                case 3:
                    timeFrameStr = "20:59:59";
                    break;
            }
            return $"2018-06-{day} {timeFrameStr}";
        }
    }
}
