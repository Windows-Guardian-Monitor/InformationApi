using BlazorBootstrap;
using System.Runtime.CompilerServices;

namespace ClientServer.Client.Models
{
	public class BarChartManager
    {
        public BarChart BarChart { get; set; } = new();
        public BarChartOptions BarChartOptions { get; set; } = new();
        public ChartData ChartData { get; set; } = new();

        private int _datasetsCount = 0;
        private int _labelsCount = 0;
        private string[] _timesOfDay = {
        "00:00",
        "01:00",
        "02:00",
        "03:00",
        "04:00",
        "05:00",
        "06:00",
        "07:00",
        "08:00",
        "09:00",
        "10:00",
        "11:00",
        "12:00",
        "13:00",
        "14:00",
        "15:00",
        "16:00",
        "17:00",
        "18:00",
        "19:00",
        "20:00",
        "21:00",
        "22:00",
        "23:00",
    };

        private Random _random = new();

        public void SetChartData(string chartName, List<int> data)
        {
            ChartData = new ChartData { Labels = GetDefaultDataLabels(), Datasets = GetDefaultDataSets(chartName, data) };
            BarChartOptions = new BarChartOptions { Responsive = true, Interaction = new Interaction { Mode = InteractionMode.Index } };
        }

        private List<IChartDataset> GetDefaultDataSets(string cpuChartName, List<int> data) => new List<IChartDataset>()
        {
            GetRandomBarChartDataset(cpuChartName, data)
        };

        private BarChartDataset GetRandomBarChartDataset(string chartName, List<int> data)
        {
            var c = ColorUtility.CategoricalTwelveColors[_datasetsCount].ToColor();

            _datasetsCount += 1;

            return new BarChartDataset()
            {
                Label = chartName,
                Data = GetData(data),
                BackgroundColor = new List<string> { c.ToRgbString() },
                BorderColor = new List<string> { c.ToRgbString() },
                BorderWidth = new List<double> { 0 },
            };
        }

        private List<double?> GetData(List<int> intData)
        {
            var data = new List<double?>();
            for (var index = 0; index < _labelsCount; index++)
            {
                //TODO CREATE DATA HERE
                //data.Add(_random.Next(100));
                data.Add(intData[index]);
            }

            return data;
        }

        private List<string> GetDefaultDataLabels()
        {
            var numberOfLabels = 24;
            var labels = new List<string>();
            for (var index = 0; index < numberOfLabels; index++)
            {
                labels.Add(GetNextDataLabel());
            }

            return labels;
        }

        private string GetNextDataLabel()
        {
            _labelsCount += 1;
            return _timesOfDay[_labelsCount - 1];
        }

        public async Task UpdateAsync(string chartName, List<int> data)
        {
            if (ChartData is null || ChartData.Datasets is null || !ChartData.Datasets.Any())
			{
                SetChartData(chartName, data);
                await InitializeChartAsync();
				return;
			}

			var newDatasets = new List<IChartDataset>();

            foreach (var dataset in ChartData.Datasets)
            {
                if (dataset is BarChartDataset barChartDataset
                    && barChartDataset is not null
                    && barChartDataset.Data is not null)
                {
                    var count = barChartDataset.Data.Count;

                    var newData = new List<double?>();
                    for (var i = 0; i < count; i++)
                    {
                        newData.Add(data[i]);
                        //newData.Add(_random.Next(200));
                    }

                    barChartDataset.Data = newData;
                    newDatasets.Add(barChartDataset);
                }
            }

            ChartData.Datasets = newDatasets;

            await BarChart.UpdateAsync(ChartData, BarChartOptions);
        }

        public async ValueTask InitializeChartAsync()
        {
			await BarChart.InitializeAsync(ChartData, BarChartOptions);
		}

        private async Task ShowHorizontalBarChartAsync()
        {
            BarChartOptions.IndexAxis = "y";
            await BarChart.UpdateAsync(ChartData, BarChartOptions);
        }

        private async Task ShowVerticalBarChartAsync()
        {
            BarChartOptions.IndexAxis = "x";
            await BarChart.UpdateAsync(ChartData, BarChartOptions);
        }
    }
}
