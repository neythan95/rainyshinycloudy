using System;
using UIKit;
using System.Net.Http;
using CoreLocation;
using System.Threading.Tasks;
using Foundation;

namespace RainyShinyCloudyTake2
{
	public partial class WeatherVC : UIViewController
	{
		TblForecastDataSource ds = new TblForecastDataSource();
		TblForecastDelegate dl = new TblForecastDelegate();
		CLLocationManager locManager = new CLLocationManager();
		CurrentWeather currentWeather = new CurrentWeather();
		UIRefreshControl refreshControl = new UIRefreshControl();

		public WeatherVC(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			tblForecast.Delegate = dl;
			tblForecast.DataSource = ds;

			locManager.AuthorizationChanged += OnAuthorizationChanged;

			refreshControl.ValueChanged += _RefreshValueChanged;
			refreshControl.AttributedTitle = new NSAttributedString("Pull down or shake your screen to refresh");

			tblForecast.Add(refreshControl);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			_RequestAuthorization();
		}

		public async override void MotionEnded(UIEventSubtype motion, UIEvent evt)
		{
			base.MotionEnded(motion, evt);

			for (int i = 1; i <= refreshControl.Frame.Height; i++)
			{
				tblForecast.SetContentOffset(new CoreGraphics.CGPoint(0, -i), true);
			}

			await _Refresh();
		}


		public async void OnAuthorizationChanged(object sender, CLAuthorizationChangedEventArgs e)
		{
			if (e.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
			{
				_CaptureLocation();
				await _FetchWeatherData();
				_BindWeatherDataToUI();
			}
			else
			{
				_RequestAuthorization();
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

			Constants.CONSTRUCT_WEATHER_URL(location.Latitude, location.Longitude);
			Constants.CONSTRUCT_LOCATION_URL(location.Latitude, location.Longitude);
		}

		private async Task _FetchWeatherData()
		{
			if (!Reachability.IsHostReachable(Constants.LOCATION_URL) || !Reachability.IsHostReachable(Constants.CURRENT_WEATHER_URL) || !Reachability.IsHostReachable(Constants.FORECAST_URL))
			{
				_CreateAndShowAlert("Please turn on Wifi or Cellular data.");
			}
			else
			{
				try
				{
					currentWeather.UpdateCurrentWeather(await _CallAPI(Constants.CURRENT_WEATHER_URL));
					currentWeather.UpdateCurrentCity(await _CallAPI(Constants.LOCATION_URL));
					ds.PopulateForecasts(await ds.CallAPI(Constants.FORECAST_URL));
				}
				catch
				{
					_CreateAndShowAlert("Cannot retrieve data from the server, please make sure that the network you are using is connected to the internet.");
				}
			}
		}

		private void _BindWeatherDataToUI()
		{
			lblToday.Text = currentWeather.Day;
			lblCity.Text = currentWeather.City;
			lblCurrentTemp.Text = $"{currentWeather.Temperature}°";
			lblWeatherType.Text = currentWeather.WeatherType;
			imgWeatherType.Image = UIImage.FromBundle(currentWeather.WeatherType);

			tblForecast.ReloadData();
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

		private async void _RefreshValueChanged(object sender, EventArgs args)
		{
			await _Refresh();
		}

		private async Task _Refresh()
		{
			refreshControl.BeginRefreshing();

			_CaptureLocation();
			await _FetchWeatherData();

			refreshControl.EndRefreshing();

			_BindWeatherDataToUI();
		}
		#endregion
	}
}