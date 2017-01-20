using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RainyShinyCloudyTake2
{
	public class Forecast
	{
		#region PROPERTIES
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

		public int TempHigh
		{
			get;
			set;
		}

		public int TempLow
		{
			get;
			set;
		}
		#endregion

		public Forecast()
		{
			this.Day = "";
			this.WeatherType = "";
			this.TempHigh = 0;
			this.TempLow = 0;
		}

		public Forecast(Dictionary<string, object> rawForecast)
		{
			this.UpdateForecast(rawForecast);
		}

		public void UpdateForecast(Dictionary<string, object> rawForecast)
		{
			Double date = Convert.ToDouble(rawForecast["dt"]);
			var temperature = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawForecast["temp"].ToString());
			var weatherArray = JsonConvert.DeserializeObject<object[]>(rawForecast["weather"].ToString());
			var weatherType = JsonConvert.DeserializeObject<Dictionary<string, object>>(weatherArray[0].ToString());

			this.Day = _GetDay(date);
			this.WeatherType = _GetWeatherType(weatherType);
			this.TempHigh = _GetTempHigh(temperature);
			this.TempLow = _GetTempLow(temperature);
		}

		#region PRIVATE METHODS
		private string _GetDay(Double unixTimeStamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp);

			string today = String.Format("{0:dddd}", dateTime);

			return today;
		}

		private string _GetWeatherType(Dictionary<string, object> weatherType)
		{
			return weatherType["main"].ToString();
		}

		private int _GetTempHigh(Dictionary<string, object> temperature)
		{
			var tempHigh = _ConvertKelvinToCelcius(Convert.ToDouble(temperature["max"]));

			return Convert.ToInt32(tempHigh);
		}

		private int _GetTempLow(Dictionary<string, object> temperature)
		{
			var tempLow = _ConvertKelvinToCelcius(Convert.ToDouble(temperature["min"]));

			return Convert.ToInt32(tempLow);
		}

		private double _ConvertKelvinToCelcius(double kelvin)
		{
			var kelvinToCelcius = kelvin - 273.15;

			return kelvinToCelcius;
		}
		#endregion
	}
}