using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Pattern
    {
        public List<Point> listPoint { get; set; }

        public Pattern()
        {
            listPoint = new List<Point>();
        }
        public Pattern(int lenghtOFWindow)
        {
            init(lenghtOFWindow);
        }

        public void init(int lenghtOFWindow)
        {
            listPoint = new List<Point>(lenghtOFWindow);
        }

        public Point getLastPoint()
        {
            return this.listPoint[listPoint.Count - 1];
        }
    }
}
