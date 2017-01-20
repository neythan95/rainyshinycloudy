using Foundation;
using System;
using UIKit;
using System.Net.Http;
using CoreLocation;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System.Xml;

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

			tblForecast.Delegate = new TblForecastDelegate();
			tblForecast.DataSource = new TblForecastDataSource();

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

				HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, Constants.URL);

				HttpClient client = new HttpClient();
				HttpResponseMessage result = await client.SendAsync(msg);

				string json = await result.Content.ReadAsStringAsync();

				CurrentWeather currentWeather = new CurrentWeather(json);
				_BindCurrentWeatherToUI(currentWeather);
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

			Constants.ConstructURL(location.Latitude, location.Longitude);
		}

		private void _BindCurrentWeatherToUI(CurrentWeather currentWeather)
		{
			lblToday.Text = currentWeather.Day;
			lblCity.Text = currentWeather.City;
			lblCurrentTemp.Text = $"{Convert.ToInt32(currentWeather.Temperature)}°";
			lblWeatherType.Text = currentWeather.WeatherType;
			imgWeatherType.Image = UIImage.FromBundle(currentWeather.WeatherType);
		}
		#endregion
	}
}