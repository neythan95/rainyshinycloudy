using System;
using UIKit;
using System.Net.Http;
using CoreLocation;
using System.Threading.Tasks;

namespace RainyShinyCloudyTake2
{
	public partial class WeatherVC : UIViewController
	{
		TblForecastDataSource ds = new TblForecastDataSource();
		TblForecastDelegate dl = new TblForecastDelegate();

		CLLocationManager locManager = new CLLocationManager();
		CLAuthorizationStatus authorizationStatus;

		public WeatherVC(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			tblForecast.Delegate = dl;
			tblForecast.DataSource = ds;

			locManager.AuthorizationChanged += OnAuthorizationChanged;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			_RequestAuthorization();
		}

		public async void OnAuthorizationChanged(object sender, CLAuthorizationChangedEventArgs args)
		{
			authorizationStatus = args.Status;

			if (authorizationStatus == CLAuthorizationStatus.AuthorizedWhenInUse)
			{
				_CaptureLocation();

				if (Reachability.InternetConnectionStatus() == NetworkStatus.NotReachable)
				{
					_CreateAndShowAlert("Please turn on Wifi or Cellular data.");
				}
				else
				{
					try
					{
						var currentWeather = new CurrentWeather(await _CallAPI(Constants.CURRENT_WEATHER_URL));
						ds.PopulateForecasts(await ds.CallAPI(Constants.FORECAST_URL));

						_BindCurrentWeatherToUI(currentWeather);
						tblForecast.ReloadData();
					}
					catch
					{
						_CreateAndShowAlert("Cannot retrieve data from the server, please make sure that the network you are using is connected to the internet.");
					}
				}
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
			string json = "";

			try
			{
				HttpResponseMessage response = await (client.SendAsync(msg));
				json = await response.Content.ReadAsStringAsync();
			}
			catch { }

			return json;
		}

		private void _CreateAndShowAlert(string msg)
		{
			UIAlertView alert = new UIAlertView();
			alert.Message = msg;
			alert.AddButton("ok");

			alert.Show();
		}
		#endregion
	}
}