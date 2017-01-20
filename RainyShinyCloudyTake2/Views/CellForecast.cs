using Foundation;
using System;
using UIKit;

namespace RainyShinyCloudyTake2
{
	public partial class CellForecast : UITableViewCell
	{
		public CellForecast(IntPtr handle) : base(handle)
		{

		}

		public void BindForecastToCell(Forecast forecast)
		{
			lblDay.Text = forecast.Day;
			lblWeatherType.Text = forecast.WeatherType;
			imgWeatherType.Image = UIImage.FromBundle(forecast.WeatherType);
			lblTempHigh.Text = $"{forecast.TempHigh}°";
			lblTempLow.Text = $"{forecast.TempLow}°";
		}
	}
}