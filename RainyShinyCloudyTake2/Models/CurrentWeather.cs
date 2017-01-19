using System;
namespace RainyShinyCloudyTake2
{
	public class CurrentWeather
	{
		public string Day
		{
			get;
			set;
		}

		public double Temperature
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}

		public string WeatherType
		{
			get;
			set;
		}

		public CurrentWeather()
		{
			this.Day = "";
			this.Temperature = 0;
			this.City = "";
			this.WeatherType = "";
		}
	}
}
