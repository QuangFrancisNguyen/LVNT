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
        List<DataPrediction> dataDislay;
        public LineChart(List<DataPrediction> data)
        {
            Init(data);
        }

        private void Init(List<DataPrediction> data)
        {
            components = new Container();
            ChartArea ar = new ChartArea();
            Legend lg = new Legend();
            lineChart = new Chart();
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
            this.lineChart.TabIndex = 0;
            this.lineChart.Text = "LineChart";

            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
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
                ChartType = SeriesChartType.Line
            };
            var serriLineChart = new Series
            {
                Name = "Data",
                Color = Color.Green,           
                ChartType = SeriesChartType.Line
            };
            this.lineChart.Series.Add(serriLineChart);           
            this.lineChart.Series.Add(newLinePredic);
            this.lineChart.Series.Add(serriPoint);
            
            for(int i=0 ;i < dataDislay.Count ; i++)
            {
                if (!dataDislay[i].isNewPoint)
                {
                    serriLineChart.Points.AddXY(dataDislay[i].timeOfValue.Date, dataDislay[i].value);
                }
                else
                {
                    if (Added)
                    {
                        Added = !Added;
                        newLinePredic.Points.AddXY(dataDislay[i-1].timeOfValue.Date, dataDislay[i-1].value);
                    }
                    newLinePredic.Points.AddXY(dataDislay[i].timeOfValue.Date, dataDislay[i].value);
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
        }
        protected void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
