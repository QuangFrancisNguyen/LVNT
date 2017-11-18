using System.Collections.Generic;
using System.Windows;
using Common;
using Algorithm;
using Algorithm.Charts;
using System.Windows.Forms;
using System;

namespace LVTN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btn_KNN(object sender, RoutedEventArgs e)
        {

            int checkresutl = 0;
            var lp = int.Parse(txt_LenghtOfPattern.Text);
            var kNN = int.Parse(txt_KNN.Text);
            var rate = float.Parse(txt_rateIP.Text);
            var numPre = int.Parse(txt_NumOfPredic.Text);
            var dataPrediction = txt_SelectedData.Text;
            checkresutl = CheckDataLenghPattern(lp);
            checkresutl = CheckValueKNN(kNN);
            checkresutl = CheckRateImportantPoint(rate);
            checkresutl = CheckPredictionHorizon(numPre);
            checkresutl = CheckDataInput(dataPrediction);
            if (checkresutl.Equals(0))
            {
                List<DataPrediction> dataNeedPre = new List<DataPrediction>();
                List<DataPrediction> dataAfterPre = new List<DataPrediction>();
                List<DataPrediction> actualData = new List<DataPrediction>();
                KNearestNeighbor KNN = new KNearestNeighbor(lp, kNN, rate);
                //actualData = Function.ReadCSVFile(@"F:\TTTN_LVTN\Duong Tuan Anh\SourceCode\LVTNDTA\Data\LEADING_ECONOMIC.csv");
                dataNeedPre = Function.ReadCSVFile(dataPrediction);
                dataAfterPre = KNN.PredictWithKNN(dataNeedPre);
                for (int i = 1; i < numPre; i++)
                {
                    Function.RefeshData(dataAfterPre);
                    dataAfterPre = KNN.PredictWithKNN(dataAfterPre);
                }
                //Function.MAE(actualData, dataAfterPre);
                LineChart chart = new LineChart(dataAfterPre);
                chart.Show();
            }
        }
        private void btn_HotWinter(object sender, RoutedEventArgs e)
        {
            this.Title = txt_KNN.Text;
        }
        private void btn_Browser(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dataFile = new OpenFileDialog();
            dataFile.InitialDirectory = @"C:\";
            dataFile.Title = "Select Prediction Data";
            dataFile.CheckFileExists = true;
            dataFile.CheckPathExists = true;
            dataFile.DefaultExt = "csv";
            dataFile.Filter = "Text files (*.csv)|*.csv";
            dataFile.FilterIndex = 2;
            dataFile.RestoreDirectory = true;
            dataFile.ReadOnlyChecked = true;
            dataFile.ShowReadOnly = true;
            if (dataFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txt_SelectedData.Text = dataFile.FileName;
            }
        }
        private int CheckDataLenghPattern(int value)
        {
            if (value <= 1)
            {
                System.Windows.MessageBox.Show("Number Important Point in Each Pattern must be great than one",
                    "Error Lenght of Pattern", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return -1;
            }
            else return 0;
        }
        private int CheckRateImportantPoint(double value)
        {
            if (value <= 1)
            {
                System.Windows.MessageBox.Show("Rate Important great than one",
                    "Error Rate ImportantPoint", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return -1;
            }
            else return 0;
        }
        private int CheckValueKNN(int value)
        {
            if (value <= 0)
            {
                System.Windows.MessageBox.Show("Number K Nearest Neightbough must be great than zero",
                    "Error Number KNN", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return -1;
            }
            else return 0;
        }
        private int CheckPredictionHorizon(int value)
        {
            if(value <= 0)
            {
                System.Windows.MessageBox.Show("Number of Prediction Horizon must be great than zero",
                     "Error Number of Prediction Horizon", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return -1;
            }else return 0;
        }
        private int CheckDataInput(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                System.Windows.MessageBox.Show("Please Input data",
                     "Data input", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return -1;
            }
            else return 0;
        }
    }
}
