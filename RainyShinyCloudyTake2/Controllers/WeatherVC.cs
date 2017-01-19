using Foundation;
using System;
using UIKit;
using System.Net.Http;
using CoreLocation;

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

		void OnAuthorizationChanged(object sender, CLAuthorizationChangedEventArgs args)
		{
			authorizationStatus = args.Status;

			if (authorizationStatus == CLAuthorizationStatus.AuthorizedWhenInUse)
			{
				locManager.StartUpdatingLocation();

				location.Latitude = locManager.Location.Coordinate.Latitude;
				location.Longitude = locManager.Location.Coordinate.Longitude;

				Console.WriteLine($"{location.Latitude} {location.Longitude}");
			}
		}
	}
}