﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitScry.Extensions;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace bitScry.Controllers.Projects
{
    [Route("Projects/[controller]/[action]")]
    public class ChartController : Controller
    {
        private readonly IConfiguration _config;

        public ChartController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("/Projects/[controller]")]
        [ActionName("Index")]
        public IActionResult Index()
        {
            Chart barChart = GenerateBarChart();
            Chart lineChart = GenerateLineChart();
            Chart lineScatterChart = GenerateLineScatterChart();
            Chart radarChart = GenerateRadarChart();
            Chart polarChart = GeneratePolarChart();
            Chart pieChart = GeneratePieChart();
            Chart nestedDoughnutChart = GenerateNestedDoughnutChart();

            ViewData["BarChart"] = barChart;
            ViewData["LineChart"] = lineChart;
            ViewData["LineScatterChart"] = lineScatterChart;
            ViewData["RadarChart"] = radarChart;
            ViewData["PolarChart"] = polarChart;
            ViewData["PieChart"] = pieChart;
            ViewData["NestedDoughnutChart"] = nestedDoughnutChart;

            return View("~/Views/Projects/Chart/Index.cshtml");
        }

        [ActionName("GetRandomData")]
        public int[] GetRandomData(int id)
        {
            List<int> data = new List<int>();

            for (int i = 0; i < id; i++)
            {
                data.Add(AppCode.Home.GetRandomInteger(0, 100));
            }

            return data.ToArray();
        }

        private static Chart GenerateBarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            Data data = new Data();
            data.Labels = new List<string>() { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };

            BarDataset dataset = new BarDataset()
            {
                Label = "# of Votes",
                Data = new List<double>() { 12, 19, 3, 5, 2, 3 },
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgba(255, 99, 132, 0.2),
                    ChartColor.FromRgba(54, 162, 235, 0.2),
                    ChartColor.FromRgba(255, 206, 86, 0.2),
                    ChartColor.FromRgba(75, 192, 192, 0.2),
                    ChartColor.FromRgba(153, 102, 255, 0.2),
                    ChartColor.FromRgba(255, 159, 64, 0.2)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(255, 99, 132),
                    ChartColor.FromRgb(54, 162, 235),
                    ChartColor.FromRgb(255, 206, 86),
                    ChartColor.FromRgb(75, 192, 192),
                    ChartColor.FromRgb(153, 102, 255),
                    ChartColor.FromRgb(255, 159, 64)
                },
                BorderWidth = new List<int>() { 1 }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                Scales = new Scales()
            };

            var scales = new Scales
            {
                YAxes = new List<Scale>
                {
                    new CartesianScale
                    {
                        Ticks = new CartesianLinearTick
                        {
                            BeginAtZero = true
                        }
                    }
                },
                XAxes = new List<Scale>
                {
                    new BarScale
                    {
                        BarPercentage = 0.5,
                        BarThickness = 6,
                        MaxBarThickness = 8,
                        MinBarLength = 2,
                        GridLines = new GridLine()
                        {
                            OffsetGridLines = true
                        }
                    }
                }
            };

            options.Scales = scales;

            chart.Options = options;

            chart.Options.Layout = new Layout
            {
                Padding = new Padding
                {
                    PaddingObject = new PaddingObject
                    {
                        Left = 10,
                        Right = 12
                    }
                }
            };

            return chart;
        }

        private static Chart GenerateLineChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Line;

            Data data = new Data();
            data.Labels = new List<string>() { "January", "February", "March", "April", "May", "June", "July" };

            LineDataset dataset = new LineDataset()
            {
                Label = "My First dataset",
                Data = new List<double>() { 65, 59, 80, 81, 56, 55, 40 },
                Fill = "false",
                LineTension = 0.1,
                BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                BorderColor = ChartColor.FromRgba(75,192,192,1),
                BorderCapStyle = "butt",
                BorderDash = new List<int> { },
                BorderDashOffset = 0.0,
                BorderJoinStyle = "miter",
                PointBorderColor = new List<ChartColor>() { ChartColor.FromRgba(75,192,192,1) },
                PointBackgroundColor = new List<ChartColor>() { ChartColor.FromHexString("#fff") },
                PointBorderWidth = new List<int> { 1 },
                PointHoverRadius = new List<int> { 5 },
                PointHoverBackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(75,192,192,1) },
                PointHoverBorderColor = new List<ChartColor>() { ChartColor.FromRgba(220,220,220,1) },
                PointHoverBorderWidth = new List<int> { 2 },
                PointRadius = new List<int> { 1 },
                PointHitRadius = new List<int> { 10 },
                SpanGaps = false
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            Options options = new Options()
            {
                Scales = new Scales()
            };

            Scales scales = new Scales()
            {
                YAxes = new List<Scale>()
                {
                    new CartesianScale()
                }
            };

            CartesianScale yAxes = new CartesianScale()
            {
                Ticks = new Tick()
            };

            Tick tick = new Tick()
            {
                Callback = "function(value, index, values) {return '$' + value;}"
            };

            yAxes.Ticks = tick;
            scales.YAxes = new List<Scale>() { yAxes };
            options.Scales = scales;
            chart.Options = options;

            chart.Data = data;

            return chart;
        }

        private static Chart GenerateLineScatterChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Line;

            Data data = new Data();

            LineScatterDataset dataset = new LineScatterDataset()
            {
                Label = "Scatter Dataset",
                Data = new List<LineScatterData>()
            };

            LineScatterData scatterData1 = new LineScatterData();
            LineScatterData scatterData2 = new LineScatterData();
            LineScatterData scatterData3 = new LineScatterData();

            scatterData1.X = "-10";
            scatterData1.Y = "0";
            dataset.Data.Add(scatterData1);

            scatterData2.X = "0";
            scatterData2.Y = "10";
            dataset.Data.Add(scatterData2);

            scatterData3.X = "10";
            scatterData3.Y = "5";
            dataset.Data.Add(scatterData3);

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            Options options = new Options()
            {
                Scales = new Scales()
            };

            Scales scales = new Scales()
            {
                XAxes = new List<Scale>()
                {
                    new CartesianScale()
                    {
                        Type = "linear",
                        Position = "bottom",
                        Ticks = new CartesianLinearTick()
                        {
                            BeginAtZero = true
                        }
                    }
                }
            };

            options.Scales = scales;

            chart.Options = options;

            return chart;
        }

        private static Chart GenerateRadarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Radar;

            Data data = new Data();
            data.Labels = new List<string>() { "Eating", "Drinking", "Sleeping", "Designing", "Coding", "Cycling", "Running" };

            RadarDataset dataset1 = new RadarDataset()
            {
                Label = "My First dataset",
                BackgroundColor = ChartColor.FromRgba(179,181,198,0.2),
                BorderColor = ChartColor.FromRgba(179,181,198,1),
                PointBackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(179,181,198,1) },
                PointBorderColor = new List<ChartColor>() { ChartColor.FromHexString("#fff") },
                PointHoverBackgroundColor = new List<ChartColor>() { ChartColor.FromHexString("#fff") },
                PointHoverBorderColor = new List<ChartColor>() { ChartColor.FromRgba(179,181,198,1) },
                Data = new List<double>() { 65, 59, 80, 81, 56, 55, 40 }
            };

            RadarDataset dataset2 = new RadarDataset()
            {
                Label = "My Second dataset",
                BackgroundColor = ChartColor.FromRgba(255,99,132,0.2),
                BorderColor = ChartColor.FromRgba(255,99,132,1),
                PointBackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(255,99,132,1) },
                PointBorderColor = new List<ChartColor>() { ChartColor.FromHexString("#fff") },
                PointHoverBackgroundColor = new List<ChartColor>() { ChartColor.FromHexString("#fff") },
                PointHoverBorderColor = new List<ChartColor>() { ChartColor.FromRgba(255,99,132,1) },
                Data = new List<double>() { 28, 48, 40, 19, 96, 27, 100 }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset1);
            data.Datasets.Add(dataset2);

            chart.Data = data;

            return chart;
        }

        private static Chart GeneratePolarChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.PolarArea;

            Data data = new Data();
            data.Labels = new List<string>() { "Red", "Green", "Yellow", "Grey", "Blue" };

            PolarDataset dataset = new PolarDataset()
            {
                Label = "My dataset",
                BackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#FF6384"),
                    ChartColor.FromHexString("#4BC0C0"),
                    ChartColor.FromHexString("#FFCE56"),
                    ChartColor.FromHexString("#E7E9ED"),
                    ChartColor.FromHexString("#36A2EB")
                },
                Data = new List<double>() { 11, 16, 7, 3, 14 }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            return chart;
        }

        private static Chart GeneratePieChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Pie;

            Data data = new Data();
            data.Labels = new List<string>() { "Red", "Blue", "Yellow" };

            PieDataset dataset = new PieDataset()
            {
                Label = "My dataset",
                BackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#FF6384"),
                    ChartColor.FromHexString("#36A2EB"),
                    ChartColor.FromHexString("#FFCE56")
                },
                HoverBackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#FF6384"),
                    ChartColor.FromHexString("#36A2EB"),
                    ChartColor.FromHexString("#FFCE56")
                },
                Data = new List<double>() { 300, 50, 100 }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            return chart;
        }

        private static Chart GenerateNestedDoughnutChart()
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Doughnut;

            Data data = new Data();
            data.Labels = new List<string>() {
                "resource-group-1",
                "resource-group-2",
                "Data Services - Basic Database Days",
                "Data Services - Basic Database Days",
                "Azure App Service - Basic Small App Service Hours",
                "resource-group-2 - Other"
            };

            PieDataset outerDataset = new PieDataset()
            {
                BackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#3366CC"),
                    ChartColor.FromHexString("#DC3912"),
                    ChartColor.FromHexString("#FF9900"),
                    ChartColor.FromHexString("#109618"),
                    ChartColor.FromHexString("#990099"),
                    ChartColor.FromHexString("#3B3EAC")
                },
                HoverBackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#3366CC"),
                    ChartColor.FromHexString("#DC3912"),
                    ChartColor.FromHexString("#FF9900"),
                    ChartColor.FromHexString("#109618"),
                    ChartColor.FromHexString("#990099"),
                    ChartColor.FromHexString("#3B3EAC")
                },
                Data = new List<double>() {
                    0.0,
                    0.0,
                    8.31,
                    10.43,
                    84.69,
                    0.84
                }
            };

            PieDataset innerDataset = new PieDataset()
            {
                BackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#3366CC"),
                    ChartColor.FromHexString("#DC3912"),
                    ChartColor.FromHexString("#FF9900"),
                    ChartColor.FromHexString("#109618"),
                    ChartColor.FromHexString("#990099"),
                    ChartColor.FromHexString("#3B3EAC")
                },
                HoverBackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#3366CC"),
                    ChartColor.FromHexString("#DC3912"),
                    ChartColor.FromHexString("#FF9900"),
                    ChartColor.FromHexString("#109618"),
                    ChartColor.FromHexString("#990099"),
                    ChartColor.FromHexString("#3B3EAC")
                },
                Data = new List<double>() {
                    8.31,
                    95.96
                }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(outerDataset);
            data.Datasets.Add(innerDataset);

            chart.Data = data;

            return chart;
        }
    }
}