using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Drawing;
using Common;
using System.Collections.Generic;

namespace Algorithm.Charts
{
    public class LineChart : Form
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
            ar.BorderColor = Color.HotPink;

            //Init for chart
            ar.Name = "PredictionChart";
            this.lineChart.ChartAreas.Add(ar);
            this.lineChart.Dock = DockStyle.Fill;
            lg.Name = "PredictionLengh";
            this.lineChart.Legends.Add(lg);
            this.lineChart.Location = new System.Drawing.Point(0, 0);
            this.lineChart.Name = "EURO_FOREIGN";
            this.lineChart.TabIndex = 0;
            this.lineChart.Text = "lineChart";
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.lineChart);
            this.Name = "lineChart";
            this.Text = "lineChart";
            this.Load += new System.EventHandler(this.LoadChart);
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            this.ResumeLayout(false);

        }
        private void LoadChart(object sender, EventArgs e)
        {
            lineChart.Series.Clear();
            var serri = new Series
            {
                Name = "serri",
                Color = Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,             
                ChartType = SeriesChartType.Line
            };
            this.lineChart.Series.Add(serri);
            foreach (var dt in dataDislay)
            {
                serri.Points.AddXY(dt.timeOfValue.Date, dt.value);  
            }
            for(int i = 0; i < dataDislay.Count; i++)
            {
                if (dataDislay[i].isIPoint)
                {
                    serri.Points[i].Color = Color.Red;
                }
            }
            
            lineChart.Invalidate();
        }
        protected void Dispose()
        {
            if (components != null)
            {
                components.Dispose();
            }
            base.Dispose(true);
        }
    }
}
