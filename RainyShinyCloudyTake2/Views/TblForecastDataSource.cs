using System;
using UIKit;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RainyShinyCloudyTake2
{
	public class TblForecastDataSource : UITableViewDataSource
	{
		public List<Forecast> forecasts = new List<Forecast>();

		public TblForecastDataSource()
		{

		}

		#region CONTRACTS
		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return forecasts.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			CellForecast cell = (CellForecast)tableView.DequeueReusableCell("CellForecast", indexPath);
			cell.BindForecastToCell(forecasts[indexPath.Row]);

			return cell;
		}
		#endregion

		public async Task<string> CallAPI(string url)
		{
			HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, url);

			HttpClient client = new HttpClient();
			HttpResponseMessage result = await client.SendAsync(msg);

			string json = await result.Content.ReadAsStringAsync();

			return json;
		}

		public void PopulateForecasts(string json)
		{
			forecasts = new List<Forecast>();

			var forecast = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
			var forecastArray = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(forecast["list"].ToString());

			foreach (var rawForecast in forecastArray)
			{
				forecasts.Add(new Forecast(rawForecast));
			}

			forecasts.RemoveAt(0);
		}
	}
}
