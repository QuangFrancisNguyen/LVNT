using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Common
{
    public class Function
    {       
        public static List<DataPrediction> ReadCSVFile(String filepath)
        {
            List<DataPrediction> newData = new List<DataPrediction>();
            try {     
                TextReader reader = File.OpenText(@filepath);
                var csv = new CsvReader(reader);
                while (csv.Read())
                {
                    DataPrediction data = new DataPrediction(csv.GetField<DateTime>(0), csv.GetField<float>(1));
                    newData.Add(data);
                }            
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            return newData;
        }
        public static void RefeshData(List<DataPrediction> datas)
        {
            foreach (var data in datas)
            {
                data.isIPoint = false;
            }
        }
        
        public static void AvgLenghPattern(List<Pattern> listPatter)
        {
            int totalPattern = listPatter.Count;
            int totalLenghtAllPattern = 0;
            foreach(var pt in listPatter)
            {
                totalLenghtAllPattern += pt.listPoint[pt.listPoint.Count - 1].index - pt.listPoint[0].index;
            }
            Console.WriteLine("*******Avg Lenght of pattern********** : [{0}]",(float)totalLenghtAllPattern / totalPattern);
        }

        public static void MAE(List<DataPrediction> actual , List<DataPrediction> predict)
        {
            var index = predict.Count - 1;
            //Console.WriteLine("*******VALUE Actual********** : [{0}]", (float)actual[index].value );
            //Console.WriteLine("*******VALUE Predict********** : [{0}]", (float)predict[index].value);
            //Console.WriteLine("*******MAE********** : [{0}]", (float)Math.Abs(actual[index].value - predict[index].value));
        }
    }
}
