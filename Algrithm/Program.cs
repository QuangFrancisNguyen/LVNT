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
            List<DataPrediction> dataNeedPre = new List<DataPrediction>();
            List<DataPrediction> dataAfterPre = new List<DataPrediction>();
            KNearestNeighbor KNN = new KNearestNeighbor(Constants.LENGH_OF_WINDOW, Constants.KNN, Constants.numSeque, Constants.R);
            dataNeedPre = Function.ReadCSVFile(Constants.ECONOMIC);
            dataAfterPre = KNN.PredictWithKNN(dataNeedPre);
            Application.Run(new LineChart(dataAfterPre));
        }
    }
}
