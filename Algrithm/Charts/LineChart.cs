using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Drawing;
using Common;
using System.Collections.Generic;

namespace Algorithm.Charts
{
    public partial class LineChart : Form
    {
        private IContainer components = null;
        private Chart lineChart;
        private ListBox lBox;
        List<DataPrediction> dataDislay;
        ToolTip tooltip;

        public LineChart(List<DataPrediction> data)
        {
            InitializeComponent(data);
        }

        private void InitializeComponent(List<DataPrediction> data)
        {
            components = new Container();
            tooltip = new ToolTip();
            ChartArea ar = new ChartArea();
            Legend lg = new Legend();
            lineChart = new Chart();
            lBox = new ListBox();
            this.dataDislay = data;
            ((ISupportInitialize)(this.lineChart)).BeginInit();
            this.SuspendLayout();
            ar.AxisX.LabelStyle.Format = "yyyy";
            ar.AxisX.Interval = 800;
            ar.AxisX.IsStartedFromZero = false;
            ar.AxisY.IsStartedFromZero = false;
            ar.Name = "PredictionChart";

            //Init for chart
            lg.Name = "PredictionLengh";
            this.lineChart.ChartAreas.Add(ar);
            this.lineChart.Dock = DockStyle.Fill;
            this.lineChart.Legends.Add(lg);
            this.lineChart.Location = new System.Drawing.Point(0, 0);
            this.lineChart.Name = "LineChart";
            this.lineChart.Text = "LineChart";
            this.lineChart.TabIndex = 1;
            this.tooltip.AutoPopDelay = 500;
            this.tooltip.InitialDelay = 100;

            //For list box
            this.lBox.FormattingEnabled = true;
            this.lBox.Location = new System.Drawing.Point(825, 150);
            this.lBox.Name = "listBox";
            this.lBox.Size = new System.Drawing.Size(170, 300);
            this.lBox.TabIndex = 0;
            this.lBox.Anchor = AnchorStyles.Right;
            //
            // Form1
            //
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.lBox);
            this.Controls.Add(this.lineChart);
            this.Name = "Prediction";
            this.Text = "Prediction";
            this.Load += new System.EventHandler(this.LoadChart);
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            this.ResumeLayout(false);

        }

        private void LoadChart(object sender, EventArgs e)
        {
            bool Added = true;
            lineChart.Series.Clear();

            int countNewLinePredic = 1;

            var serriPoint = new Series
            {
                Name = "Important Point",
                Color = Color.Blue,
                ChartType = SeriesChartType.Point             
            };
            var newLinePredic = new Series
            {
                Name = "Predicted Data",
                Color = Color.Red,
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            };
            var serriLineChart = new Series
            {
                Name = "Data",
                Color = Color.Green,
                ChartType = SeriesChartType.Line,
                BorderWidth = 2     
            };           
            this.lineChart.Series.Add(serriPoint);
            this.lineChart.Series.Add(serriLineChart);
            this.lineChart.Series.Add(newLinePredic);
            for (int i=0 ;i < dataDislay.Count ; i++)
            {
                if (!dataDislay[i].isNewPoint)
                {
                    serriLineChart.Points.AddXY(dataDislay[i].timeOfValue.Date, dataDislay[i].value);
                    serriLineChart.Points[i].ToolTip = string.Format(" Date: [{0}] \nValue: [{1}]", dataDislay[i].timeOfValue.ToShortDateString(), dataDislay[i].value);
                }
                else
                {
                    if (Added)
                    {
                        Added = !Added;
                        newLinePredic.Points.AddXY(dataDislay[i-1].timeOfValue.Date, dataDislay[i-1].value);
                    }
                    newLinePredic.Points.AddXY(dataDislay[i].timeOfValue.Date, dataDislay[i].value);
                    newLinePredic.Points[countNewLinePredic].ToolTip = string.Format(" Date: [{0}] \nValue: [{1}]", dataDislay[i].timeOfValue.ToShortDateString(), dataDislay[i].value); 
                }
                 
            }          
            foreach (var dt in dataDislay)
            {

                if (dt.isIPoint)
                {
                    serriPoint.Points.AddXY(dt.timeOfValue.Date, dt.value);
                }
            }       
            lineChart.Invalidate();
            lBox.DataSource = addDataForListBox();
        }

        private List<string> addDataForListBox()
        {
            List<string> dataDislayed = new List<string>();
            foreach(var data in dataDislay)
            {
                if (data.isNewPoint)
                {
                    dataDislayed.Add(string.Format("[{0}] : [{1}]",data.timeOfValue.ToShortDateString(),data.value.ToString()));
                }
            }
            return dataDislayed;
        }
    
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
