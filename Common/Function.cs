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
    }
}
