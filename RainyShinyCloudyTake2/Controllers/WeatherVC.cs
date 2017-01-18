using Foundation;
using System;
using UIKit;

namespace RainyShinyCloudyTake2
{
	public partial class WeatherVC : UIViewController
    {
        public WeatherVC (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			tblForecast.Delegate = new TblForecastDelegate();
			tblForecast.DataSource = new TblForecastDataSource();
		}
    }
}