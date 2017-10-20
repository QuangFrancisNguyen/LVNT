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
        public static float R = 1.04F;
        public static int LENGH_OF_WINDOW = 5;
        public static int KNN = 5;
        public static int numSeque = LENGH_OF_WINDOW - 2;
    }
}
