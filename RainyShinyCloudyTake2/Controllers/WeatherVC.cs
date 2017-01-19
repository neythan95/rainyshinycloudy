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
		CLLocationCoordinate2D location;

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

			RequestAuthorization();
		}

		private void RequestAuthorization()
		{
			locManager.RequestWhenInUseAuthorization();
		}

		async void OnAuthorizationChanged(object sender, CLAuthorizationChangedEventArgs args)
		{
			authorizationStatus = args.Status;

			if (authorizationStatus == CLAuthorizationStatus.AuthorizedWhenInUse)
			{
				locManager.StartUpdatingLocation();

				location.Latitude = locManager.Location.Coordinate.Latitude;
				location.Longitude = locManager.Location.Coordinate.Longitude;

				Constants.ConstructURL(location.Latitude, location.Longitude);

				Console.WriteLine(Constants.URL);

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Constants.URL));
				request.ContentType = "application/json";
				request.Method = "GET";

				var response = await request.GetResponseAsync();
				Stream stream = response.GetResponseStream();

				JsonSerializerSettings settings = new JsonSerializerSettings();
				settings.Formatting = Newtonsoft.Json.Formatting.None;

				string result = JsonConvert.SerializeObject(stream.Read(), settings);

				Console.WriteLine(result);
			}
		}
	}
}