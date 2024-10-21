using BlazorBootstrap;

namespace ClientServer.Client.Models
{
	public class PieChartManager
	{
		public PieChart PieChart { get; set; } = new();
		public ChartData ChartData { get; private set; } = default!;
		public PieChartOptions PieChartOptions { get; private set; } = default!;

		private string[]? _backgroundColors;
		private int _datasetsCount = 0;
		private const int _dataLabelsCount = 2;

		private int _latestUsed = 0;


		public void SetChartData(string chartName, int available, int used)
		{
			_backgroundColors = ColorUtility.CategoricalTwelveColors;
			ChartData = new ChartData { Labels = GetDefaultDataLabels(), Datasets = GetDefaultDataSets(available, used) };
			PieChartOptions = new();
			PieChartOptions.Responsive = true;
			PieChartOptions.Plugins.Title!.Text = chartName;
			PieChartOptions.Plugins.Title.Display = true;
		}

		public async ValueTask UpdateChart(string chartName, int available, int used)
		{
			if (_latestUsed == used)
			{
				return;
			}

			_latestUsed = used;


			if (ChartData is null || ChartData.Datasets is null || !ChartData.Datasets.Any())
			{
				SetChartData(chartName, available, used);
				await InitializeChartAsync();
				return;
			}

			var newDatasets = new List<IChartDataset>();

			var newData = new List<double?>
			{
				available,
				used
			};

			var dataSet = ChartData.Datasets[0];

			var pieChartDataset = dataSet as PieChartDataset;

			pieChartDataset.Data = newData;

			newDatasets.Add(pieChartDataset);

			ChartData.Datasets = newDatasets;

			await PieChart.UpdateAsync(ChartData, PieChartOptions);
		}

		public async ValueTask InitializeChartAsync()
		{
			await PieChart.InitializeAsync(chartData: ChartData, chartOptions: PieChartOptions, plugins: new string[] { "ChartDataLabels" });
		}

		private List<string> GetDefaultDataLabels()
		{
			var labels = new List<string>
			{
				"Disponível para uso",
				"Em uso"
			};

			return labels;
		}

		private List<IChartDataset> GetDefaultDataSets(int available, int used)
		{
			var datasets = new List<IChartDataset>();

			var dataset = CreateDataset(available, used);

			dataset.Datalabels.Anchor = Anchor.End;
			datasets.Add(dataset);
			return datasets;
		}

		private PieChartDataset CreateDataset(int available, int used)
		{
			_datasetsCount += 1;

			var data = new List<double?>
			{
				available,
				used
			};

			var colors = new List<string>();
			for (var index = 0; index < _dataLabelsCount; index++)
			{
				colors.Add(_backgroundColors![index]);
			}

			return new() { Label = $"% de uso: ", Data = data, BackgroundColor = colors };
		}
	}
}
