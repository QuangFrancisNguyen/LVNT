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

        //Sequence Prediction
        Pattern sequenEstimate;

        //Lengh Of pattern
        int patternLengh;

        //Num of KNN
        int numOfKNN;
        List<int> listKNN;

        //Lenght Of Sequence
        int lenghOfSequence;

        public KNearestNeighbor(int lenghOfPattern, int numOfKNN = 2, int lenghOfSequence = 1)
        {
            this.numOfKNN = numOfKNN;
            listPoint = new FindImportantPoint();
            listPatter = new List<Pattern>();
            listKNN = new List<int>(this.numOfKNN);
            sequenEstimate = new Pattern();
            listSequence = new List<Pattern>();
            this.patternLengh = lenghOfPattern;
            this.lenghOfSequence = lenghOfSequence;
        }
        public KNearestNeighbor(List<DataPrediction> data, int lenghOfPattern, int numOfKNN, int lenghOfSequence = 1)
        {
            Init(data, lenghOfPattern, numOfKNN, lenghOfSequence);
        }

        private void Init(List<DataPrediction> data, int lenghOfPattern, int numOfKNN, int lenghOfSequence)
        {
            this.numOfKNN = numOfKNN;
            this.dataForPredict = data;
            this.patternLengh = lenghOfPattern;
            this.lenghOfSequence = lenghOfSequence;
            listPoint = new FindImportantPoint(dataForPredict);
            listPatter = new List<Pattern>();
            listImportantPoint = listPoint.GetlistImportPoint();
            listKNN = new List<int>(this.numOfKNN);
            listSequence = new List<Pattern>();
            sequenEstimate = new Pattern();
            GenListPattern();
        }
        public List<DataPrediction> PredictWithKNN()
        {
            List<DataPrediction> newData = this.dataForPredict;
            Pattern newPattern = new Pattern();
            listPoint.setData(this.dataForPredict);
            listImportantPoint = listPoint.GetlistImportPoint();
            GenListPattern();
            FindKNN();
            newPattern = CanculatorAvengerListOfSequence();
            foreach (var pnt in newPattern.listPoint)
            {
                DataPrediction dt = new DataPrediction(newData[newData.Count - 1].getTime().AddDays(pnt.lenghBeforeDay), pnt.value);
                newData.Add(dt);
            }
            return newData;
        }
        public List<DataPrediction> PredictWithKNN(List<DataPrediction> data)
        {
            List<DataPrediction> newData = new List<DataPrediction>();
            Pattern newPattern = new Pattern();
            this.dataForPredict = data;
            listPoint.setData(data);
            listImportantPoint = listPoint.GetlistImportPoint();
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
            return newData;
        }
        // To generate List of Pattern
        private void GenListPattern()
        {
            Console.WriteLine("------------BEGIN GENERATE LIST PATTERN--------------------");
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
            Console.WriteLine("------------END GENERATE LIST PATTERN--------------------");
        }
        private Pattern CanculatorAvengerListOfSequence()
        {
            Console.WriteLine("------------BEGIN CANCULATOR AVG PATTERN--------------------");
            //GenListSequence();
            GenListSequenceImportPoint();
            Pattern newPT = new Pattern(lenghOfSequence);
            foreach (var pt in listSequence)
            {
                newPT = addPattern(newPT, pt);
            }
            for (int i = 0; i < newPT.listPoint.Count; i++)
            {
                newPT.listPoint[i].lenghBeforeDay = newPT.listPoint[i].lenghBeforeDay / listSequence.Count;
                newPT.listPoint[i].value = newPT.listPoint[i].value / listSequence.Count;
            }
            Console.WriteLine("------------END CANCULATOR AVG PATTERN--------------------");
            return newPT;
        }

        private Pattern addPattern(Pattern pt1, Pattern pt2)
        {
            Console.WriteLine("------------BEGIN ADD PATTERN--------------------");
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
            Console.WriteLine("------------END ADD PATTERN --------------------");
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
            Console.WriteLine("------------END FIND K NEAREST NEIBOUGH--------------------");
        }

        //Compare two pattern
        private float ComparePattern(Pattern pt1, Pattern pt2)
        {
            Console.WriteLine("------------BEGIN COMPARE TWO PATTERN--------------------");
            float cp = 0;
            double total = 0;
            for (int i = 0; i < pt1.listPoint.Count; i++)
            {
                double div = pt1.listPoint[i].value - pt2.listPoint[i].value;
                total += Math.Pow(div, 2);
            }
            cp = (float)Math.Sqrt(total);
            Console.WriteLine("------------BEGIN END TWO PATTERN--------------------");
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
            Console.WriteLine("------------END GET ONE KNN--------------------");
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
                for (int i = 1; i < lenghOfSequence + 1; i++)
                {
                    Point iP = new Point();
                    iP.index = indexPointInPattern + i;
                    iP.time = dataForPredict[indexPointInPattern + i].getTime();
                    iP.value = dataForPredict[indexPointInPattern + i].getValue();
                    iP.lenghBeforeDay = dataForPredict[indexPointInPattern + i].getTime().Subtract(dataForPredict[indexPointInPattern + i - 1].getTime()).Days;
                    lsPoint.Add(iP);
                }
                Pattern patternSequence = new Pattern();
                patternSequence.listPoint = lsPoint;
                listSequence.Add(patternSequence);
            }
            Console.WriteLine("------------END GENERATE LIST SEQUENCE--------------------");
        }

        private void GenListSequenceImportPoint()
        {
            Console.WriteLine("------------BEGIN GENERATE LIST SEQUENCE IMPORTANT POINT--------------------");
            foreach (var eachKNN in listKNN)
            {
                List<Point> lsPoint = new List<Point>();
                var indexPointInPattern = listPatter[eachKNN].getLastPoint().index;
                for (int i = 0; i < lenghOfSequence ; i++)
                {
                    Point iP = new Point();
                    iP.index = listPatter[eachKNN + 1].listPoint[i].index;
                    iP.time = listPatter[eachKNN + 1].listPoint[i].time;
                    iP.value = listPatter[eachKNN + 1].listPoint[i].value;
                    iP.lenghBeforeDay = listPatter[eachKNN + 1].listPoint[i].time.Subtract(listPatter[eachKNN].listPoint[i].time).Days;
                    lsPoint.Add(iP);
                }
                Pattern patternSequence = new Pattern();
                patternSequence.listPoint = lsPoint;
                listSequence.Add(patternSequence);
            }
            Console.WriteLine("------------END GENERATE LIST SEQUENCE IMPORTANT POINT--------------------");
        }
    }
}
