using System;
using System.Windows.Forms;
using Common;
using Algorithm.Charts;
using System.Collections.Generic;

namespace Algorithm
{
    class Program
    {
        [STAThread]
        public static void Main(string[] agrs)
        {
            //String filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Constants.EURO_FOREIGN);
            List<DataPrediction> dataNeedPre = Function.ReadCSVFile(Constants.EURO_FOREIGN);
            KNearestNeighbor KNN = new KNearestNeighbor(Constants.LENGH_OF_WINDOW, Constants.KNN, Constants.numSeque);
            List<DataPrediction> dataAfterPre = KNN.PredictWithKNN(dataNeedPre);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LineChart(dataAfterPre));
        }
    }
}
