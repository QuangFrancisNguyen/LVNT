using System.Collections.Generic;
using System.Windows;
using Common;
using Algorithm;
using Algorithm.Charts;
using System.Windows.Forms;

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
          
            var lp = int.Parse(txt_LenghtOfPattern.Text);
            var numPre = int.Parse(txt_NumOfPredic.Text);
            var kNN = int.Parse(txt_KNN.Text);
            var rateImport = float.Parse(txt_rateIP.Text);
            List<DataPrediction> dataNeedPre = new List<DataPrediction>();
            List<DataPrediction> dataAfterPre = new List<DataPrediction>();
            KNearestNeighbor KNN = new KNearestNeighbor(lp, kNN, Constants.FORECAST_RANGE, rateImport);
            dataNeedPre = Function.ReadCSVFile(Constants.ECONOMIC);
            dataAfterPre = KNN.PredictWithKNN(dataNeedPre);
            for(int i = 1; i < numPre; i++)
            {
                KNearestNeighbor KNN2 = new KNearestNeighbor(lp, kNN, Constants.FORECAST_RANGE, rateImport);
                dataAfterPre = KNN2.PredictWithKNN(dataAfterPre);
            }
            LineChart chart = new LineChart(dataAfterPre);
            chart.Show();
            
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
    }
}
