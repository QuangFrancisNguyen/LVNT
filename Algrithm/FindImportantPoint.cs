using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using log4net.Util;
namespace Algorithm
{
    public class FindImportantPoint 
    {

        List<Point> listImportPoint;
        List<DataPrediction> data;
        double deepCompare;
        public FindImportantPoint()
        {
            listImportPoint = new List<Point>();
        }
        public FindImportantPoint(List<DataPrediction> insertData)
        {
            listImportPoint = new List<Point>();
            this.data = insertData;  
        }
        public void setData(List<DataPrediction> insertData)
        {
            this.data = insertData;
        }

        public List<Point> GetlistImportPoint(double R)
        {
            deepCompare = R;
            Define();
            //if(!listImportPoint.Exists(x => x.index == this.data.Count - 1))
            //{
            //    Point lastPoitn = new Point();
            //    lastPoitn.index = this.data.Count -1 ;
            //    lastPoitn.value = data[this.data.Count - 1].getValue();
            //    lastPoitn.time = data[this.data.Count - 1].getTime();
            //    this.listImportPoint.Add(lastPoitn);
            //}
            //Console.WriteLine("LAST POINT {0}---------Value---------{1}", listImportPoint[listImportPoint.Count -1 ].index, listImportPoint[listImportPoint.Count - 1].value);
            return this.listImportPoint;
        }
        private void Define()
        {
            // First ImportantPoit is Begin Point
            listImportPoint.Clear();
            int importantPoint = FindTwoFistImportPoint();

            if (importantPoint < data.Count && data[importantPoint].getValue() > data[0].getValue())
                importantPoint = FindMinPointNext(importantPoint);
            while(importantPoint < data.Count)
            {
                importantPoint = FindMaxPointNext(importantPoint);
               // if (importantPoint.Equals(data.Count - 1)) break;
                importantPoint = FindMinPointNext(importantPoint);
            }
            Console.WriteLine("TOTAL IMPORTANT POINT: {0}", listImportPoint.Count);
        }
        private int FindTwoFistImportPoint()
        {
            int iMin = 0;
            int iMax = 0;
            int idsPoint = 0;
            while ((idsPoint < data.Count)
                    && ((double)(data[idsPoint].getValue() / data[iMin].getValue()) < deepCompare)
                    && ((double)(data[iMax].getValue() / data[idsPoint].getValue()) < deepCompare))
            {
                if (data[idsPoint].getValue() < data[iMin].getValue()) iMin = idsPoint;
                else  iMax = idsPoint;
                idsPoint++;
            }
            if (iMin < iMax && iMin!= iMax)
            {
                Point ipMin = new Point();
                ipMin.index = iMin;
                ipMin.value = data[iMin].getValue();
                ipMin.time = data[iMin].getTime();
                listImportPoint.Add(ipMin);

                Point ipMax = new Point();
                ipMax.index = iMax;
                ipMax.value = data[iMax].getValue();
                ipMax.time = data[iMax].getTime();
                listImportPoint.Add(ipMax);

                Console.WriteLine("Index of iMin First {0}---------Value---------{1}", ipMin.index, ipMin.value);
                Console.WriteLine("Index of iMax First {0}---------Value---------{1}", ipMax.index, ipMax.value);
            }
            else if(iMin != iMax && iMin > iMax)
            {
                Point ipMax = new Point();
                ipMax.index = iMax;
                ipMax.value = data[iMax].getValue();
                ipMax.time = data[iMax].getTime();
                this.listImportPoint.Add(ipMax);

                Point ipMin = new Point();
                ipMin.index = iMin;
                ipMin.value = data[iMin].getValue();
                ipMin.time = data[iMin].getTime();
                this.listImportPoint.Add(ipMin);

                Console.WriteLine("Index of iMax First {0}---------Value---------{1}", ipMax.index, ipMax.value);
                Console.WriteLine("Index of iMin First {0}---------Value---------{1}", ipMin.index, ipMin.value);
            }
            return idsPoint;
        }

        // Find the first important point after the i-th point
        private int FindMinPointNext(int ith)
        {
            int iMin = ith;
            while (ith < data.Count && (double)(data[ith].getValue() / data[iMin].getValue()) < deepCompare)
            {
                if (data[ith].getValue() < data[iMin].getValue()) iMin = ith;
                ith++;
            }
            if(iMin < ith)
            {
                Point ipMin = new Point();
                ipMin.index = iMin;
                ipMin.value = data[iMin].getValue();
                ipMin.time = data[iMin].getTime();
                this.listImportPoint.Add(ipMin);
                Console.WriteLine("Index of iMin {0}---------Value---------{1}", ipMin.index, ipMin.value);
            }
            return ith;


        }
        private int FindMaxPointNext(int ith)
        {
            int iMax = ith;
            while (ith < data.Count && (double)(data[iMax].getValue()/data[ith].getValue()) < deepCompare)
            {
                if (data[ith].getValue() > data[iMax].getValue()) iMax = ith;
                ith++;
            }
            if(iMax < ith)
            {
                Point ipMax = new Point();
                ipMax.index = iMax;
                ipMax.value = data[iMax].getValue();
                ipMax.time = data[iMax].getTime();
                this.listImportPoint.Add(ipMax);
                Console.WriteLine("Index of iMax {0}---------Value---------{1}", ipMax.index, ipMax.value);
            }
            return ith;
        }
    }
}

