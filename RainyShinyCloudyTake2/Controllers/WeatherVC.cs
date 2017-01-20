using System;
using UIKit;
using System.Net.Http;
using CoreLocation;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RainyShinyCloudyTake2
{
	public partial class WeatherVC : UIViewController
	{
		CLAuthorizationStatus authorizationStatus;
		CLLocationManager locManager;

		public WeatherVC(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			locManager = new CLLocationManager();
			locManager.AuthorizationChanged += OnAuthorizationChanged;

			_RequestAuthorization();
		}

		async void OnAuthorizationChanged(object sender, CLAuthorizationChangedEventArgs args)
		{
			authorizationStatus = args.Status;

			if (authorizationStatus == CLAuthorizationStatus.AuthorizedWhenInUse)
			{
				_CaptureLocation();

				CurrentWeather currentWeather = new CurrentWeather(await _CallAPI(Constants.CURRENT_WEATHER_URL));
				_BindCurrentWeatherToUI(currentWeather);

				TblForecastDataSource ds = new TblForecastDataSource();
				ds.PopulateForecasts(await ds.CallAPI(Constants.FORECAST_URL));

				tblForecast.Delegate = new TblForecastDelegate();
				tblForecast.DataSource = ds;

				tblForecast.ReloadData();

				//Console.WriteLine($"{ds.forecasts[0].Day}");
				//Console.WriteLine($"{ds.forecasts[0].WeatherType}");
				//Console.WriteLine($"{ds.forecasts[0].TempLow}");
				//Console.WriteLine($"{ds.forecasts[0].TempHigh}");
			}
		}

		#region PRIVATE METHODS
		private void _RequestAuthorization()
		{
			locManager.RequestWhenInUseAuthorization();
		}

		private void _CaptureLocation()
		{
			locManager.StartUpdatingLocation();

			CLLocationCoordinate2D location = new CLLocationCoordinate2D();
			location.Latitude = locManager.Location.Coordinate.Latitude;
			location.Longitude = locManager.Location.Coordinate.Longitude;

			Constants.CONSTRUCT_URL(location.Latitude, location.Longitude);
		}

		private void _BindCurrentWeatherToUI(CurrentWeather currentWeather)
		{
			lblToday.Text = currentWeather.Day;
			lblCity.Text = currentWeather.City;
			lblCurrentTemp.Text = $"{currentWeather.Temperature}°";
			lblWeatherType.Text = currentWeather.WeatherType;
			imgWeatherType.Image = UIImage.FromBundle(currentWeather.WeatherType);
		}

		private async Task<string> _CallAPI(string url)
		{
			HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, url);

			HttpClient client = new HttpClient();
			HttpResponseMessage result = await client.SendAsync(msg);

			string json = await result.Content.ReadAsStringAsync();

			return json;
		}
		#endregion
	}
}