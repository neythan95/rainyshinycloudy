using Foundation;
using System;
using UIKit;

namespace RainyShinyCloudyTake2
{
    public partial class CellForecast : UITableViewCell
	{
		public string Day
		{
			get;
			set;
		}

		public string WeatherType
		{
			get;
			set;
		}

		public double TempLow
		{
			get;
			set;
		}

		public double TempHigh
		{
			get;
			set;
		}

		public CellForecast (IntPtr handle) : base (handle)
		{
			this.Day = "";
			this.WeatherType = "";
			this.TempLow = 0;
			this.TempHigh = 0;
        }
    }
}