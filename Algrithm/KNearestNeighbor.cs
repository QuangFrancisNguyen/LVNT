using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Algorithm
{
    public class KNearestNeighbor
    {
        //Time seri for predictin
        List<DataPrediction> dataForPredict { get; set; }

        //List all important Point
        List<Point> listImportantPoint;

        //List all pattern
        List<Pattern> listPatter;

        //List all sequence
        List<Pattern> listSequence;

        FindImportantPoint listPoint;

        //Lengh Of pattern
        int patternLengh;

        //Num of KNN
        int numOfKNN;
        List<int> listKNN;

        double deepCompare;

        public KNearestNeighbor(int lenghOfPattern, int numOfKNN,double deepCompare)
        {
            this.numOfKNN = numOfKNN;
            listPoint = new FindImportantPoint();
            listPatter = new List<Pattern>();
            listKNN = new List<int>(this.numOfKNN);
            listSequence = new List<Pattern>();
            this.patternLengh = lenghOfPattern;
            this.deepCompare = deepCompare;
        }
        public KNearestNeighbor(List<DataPrediction> data, int lenghOfPattern, int numOfKNN, double deepCompare)
        {
            Init(data, lenghOfPattern, numOfKNN, deepCompare);
        }

        private void Init(List<DataPrediction> data, int lenghOfPattern, int numOfKNN,double deepCompare)
        {
            this.numOfKNN = numOfKNN;
            this.dataForPredict = data;
            this.patternLengh = lenghOfPattern;
            listPoint = new FindImportantPoint(dataForPredict);
            listPatter = new List<Pattern>();
            listImportantPoint = listPoint.GetlistImportPoint(deepCompare);
            listKNN = new List<int>(this.numOfKNN);
            listSequence = new List<Pattern>();
            GenListPattern();
            this.deepCompare = deepCompare;
        }
        public List<DataPrediction> PredictWithKNN()
        {
            List<DataPrediction> newData = this.dataForPredict;
            Pattern newPattern = new Pattern();
            listPoint.setData(this.dataForPredict);
            listImportantPoint = listPoint.GetlistImportPoint(deepCompare);
            GenListPattern();
            FindKNN();
            newPattern = CanculatorAvengerListOfSequence();
            foreach (var pnt in newPattern.listPoint)
            {
                DataPrediction dt = new DataPrediction(newData[newData.Count - 1].getTime().AddDays(pnt.lenghBeforeDay), pnt.value);
                dt.isNewPoint = true;
                newData.Add(dt);
            }
            for (int i = 0; i < newData.Count; i++)
            {
                for (int j = 0; j < listImportantPoint.Count; j++)
                {
                    if (i.Equals(listImportantPoint[j].index))
                    {
                        newData[i].isIPoint = true;
                        break;
                    }
                }
            }
            return newData;
        }
        public List<DataPrediction> PredictWithKNN(List<DataPrediction> data)
        {
            listKNN.Clear();
            listPatter.Clear();
            listSequence.Clear();
            List<DataPrediction> newData = new List<DataPrediction>();
            Pattern newPattern = new Pattern();
            this.dataForPredict = data;
            listPoint.setData(dataForPredict);
            listImportantPoint = listPoint.GetlistImportPoint(deepCompare);
            showlistIP(listImportantPoint);
            GenListPattern();
            FindKNN();
            newPattern = CanculatorAvengerListOfSequence();
            foreach(var dt in this.dataForPredict)
            {
                newData.Add(dt);
            }
            foreach (var pnt in newPattern.listPoint)
            {
                DataPrediction dt = new DataPrediction(newData[newData.Count - 1].getTime().AddDays(pnt.lenghBeforeDay), pnt.value);
                dt.isNewPoint = true;
                newData.Add(dt);
            }
            for(int i = 0; i < newData.Count; i++)
            {
                for(int j=0;j< listImportantPoint.Count; j++)
                {
                    if (i.Equals(listImportantPoint[j].index))
                    {
                        newData[i].isIPoint = true;
                        break;
                    }
                }
            }
            Function.AvgLenghPattern(listPatter);
            return newData;
        }
        // To generate List of Pattern
        private void GenListPattern()
        {
            int lw = 0;
            int numPatter = 0;
            while (numPatter <= listImportantPoint.Count - patternLengh)
            {
                Pattern pattern = new Pattern(patternLengh);
                List<Point> listIP = new List<Point>(patternLengh);
                while (lw < patternLengh)
                {
                    Point IP = new Point(listImportantPoint[numPatter + lw].index,
                            listImportantPoint[numPatter + lw].value, listImportantPoint[numPatter + lw].time);
                    listIP.Add(IP);
                    lw++;
                }
                pattern.listPoint = listIP;
                listPatter.Add(pattern);
                numPatter++;
                lw = 0;
            }
        }
        private Pattern CanculatorAvengerListOfSequence()
        {
            GenListSequence();
            Pattern newPT = new Pattern();
            foreach (var pt in listSequence)
            {
                newPT = addPattern(newPT, pt);
            }
            for (int i = 0; i < newPT.listPoint.Count; i++)
            {
                newPT.listPoint[i].lenghBeforeDay = newPT.listPoint[i].lenghBeforeDay / listSequence.Count;
                newPT.listPoint[i].value = newPT.listPoint[i].value / listSequence.Count;
            }
            return newPT;
        }

        private Pattern addPattern(Pattern pt1, Pattern pt2)
        {
            Pattern newPT = new Pattern();
            if ((pt1.listPoint.Count != pt2.listPoint.Count)
                && (pt1.listPoint.Count != 0 && pt2.listPoint.Count != 0))
            {
                Console.WriteLine("------------LENGHT OF PATTERN NOT SAME--------------------");
            }
            else if (!(pt1.listPoint.Count != 0))
            {
                for (int i = 0; i < pt2.listPoint.Count; i++)
                {
                    Point IP = new Point();
                    IP.value = pt2.listPoint[i].value;
                    IP.lenghBeforeDay = pt2.listPoint[i].lenghBeforeDay;
                    newPT.listPoint.Add(IP);
                }
            }
            else if (!(pt2.listPoint.Count != 0))
            {
                for (int i = 0; i < pt1.listPoint.Count; i++)
                {
                    Point IP = new Point();
                    IP.value = pt1.listPoint[i].value;
                    IP.lenghBeforeDay = pt1.listPoint[i].lenghBeforeDay;
                    newPT.listPoint.Add(IP);
                }
            }
            else
            {
                for (int i = 0; i < pt2.listPoint.Count; i++)
                {
                    Point IP = new Point();
                    IP.value = pt1.listPoint[i].value + pt2.listPoint[i].value;
                    IP.lenghBeforeDay = pt1.listPoint[i].lenghBeforeDay + pt2.listPoint[i].lenghBeforeDay;
                    newPT.listPoint.Add(IP);
                }
            }
            return newPT;
        }
        // Find K nearest neibough
        private void FindKNN()
        {
            Console.WriteLine("------------BEGIN FIND K NEAREST NEIBOUGH--------------------");
            Pattern lastPattern = listPatter[listPatter.Count - 1];
            List<float> listCompare = new List<float>();
            for (int i = 0; i < listPatter.Count - 1; i++)
            {
                var cp = ComparePattern(lastPattern, listPatter[i]);
                listCompare.Add(cp);
            }
            for (int i = 0; i < numOfKNN; i++)
            {
                int indexNN = GetNN(listCompare, listKNN);
                listKNN.Add(indexNN);
            }
        }

        //Compare two pattern
        private float ComparePattern(Pattern pt1, Pattern pt2)
        {
            float cp = 0;
            double total = 0;
            for (int i = 0; i < pt1.listPoint.Count; i++)
            {
                double div = pt1.listPoint[i].value - pt2.listPoint[i].value;
                total += Math.Pow(div, 2);
            }
            cp = (float)Math.Sqrt(total);
            return cp;
        }

        //Return index of Nearest Neibough Pattern
        private int GetNN(List<float> lNN, List<int> inogreIndex)
        {
            Console.WriteLine("------------BEGIN GET ONE KNN--------------------");
            int nearestN = 0;
            for (int i = 1; i < lNN.Count; i++)
            {
                if (lNN[nearestN] > lNN[i] && inogreIndex != null)
                {
                    if (inogreIndex.Exists(x => x == i))
                    {
                        continue;
                    }
                    else
                    {
                        nearestN = i;
                    }
                }
            }
            return nearestN;
        }

        //Gene
        private void GenListSequence()
        {
            Console.WriteLine("------------BEGIN GENERATE LIST SEQUENCE--------------------");
            foreach (var eachKNN in listKNN)
            {
                List<Point> lsPoint = new List<Point>();
                var indexPointInPattern = listPatter[eachKNN].getLastPoint().index;
                Point iP = new Point();
                iP.index = indexPointInPattern + 1;
                iP.time = dataForPredict[indexPointInPattern + 1].getTime();
                iP.value = dataForPredict[indexPointInPattern + 1].getValue();
                iP.lenghBeforeDay = dataForPredict[indexPointInPattern + 1].getTime().Subtract(dataForPredict[indexPointInPattern].getTime()).Days;
                lsPoint.Add(iP);
                Pattern patternSequence = new Pattern();
                patternSequence.listPoint = lsPoint;
                listSequence.Add(patternSequence);
            }
        }

        private void GenListSequenceImportPoint()
        {
            Console.WriteLine("------------BEGIN GENERATE LIST SEQUENCE IMPORTANT POINT--------------------");
            foreach (var eachKNN in listKNN)
            {
                List<Point> lsPoint = new List<Point>();
                var indexPointInPattern = listPatter[eachKNN].getLastPoint().index;
                Point iP = new Point();
                iP.index = listPatter[eachKNN + 1].listPoint[1].index;
                iP.time = listPatter[eachKNN + 1].listPoint[1].time;
                iP.value = listPatter[eachKNN + 1].listPoint[1].value;
                iP.lenghBeforeDay = listPatter[eachKNN + 1].listPoint[1].time.Subtract(listPatter[eachKNN].listPoint[1].time).Days;
                lsPoint.Add(iP);
                Pattern patternSequence = new Pattern();
                patternSequence.listPoint = lsPoint;
                listSequence.Add(patternSequence);
            }
            Console.WriteLine("------------END GENERATE LIST SEQUENCE IMPORTANT POINT--------------------");
        }

        private void showlistIP(List<Point> listImportantPoint)
        {
            foreach(var element in listImportantPoint)
            {
                Console.WriteLine("Important Point {0}---------Value---------{1}", element.index, element.value);
            }
        }
    }
}
