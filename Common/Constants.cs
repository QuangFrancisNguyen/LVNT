using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Constants
    {
        public static String EURO_FOREIGN = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\EuroForeignExchange.csv");
        public static String ECONOMIC = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\LEADING_ECONOMIC.csv");
        public static String TEMPRATURE = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\Average-Monthly-Memperatures-AirPort.csv");
        public static float R = 1.001F;
        public static int LENGH_OF_WINDOW = 5;
        public static int KNN = 4;
        public static int FORECAST_RANGE = 1;
    }
}
