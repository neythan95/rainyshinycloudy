using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RainyShinyCloudyTake2
{
	public class CurrentWeather
	{
		#region PROPERTIES
		public string Day
		{
			get;
			set;
		}

		public int Temperature
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
		#endregion

		public CurrentWeather()
		{
			this.Day = "n/a";
			this.Temperature = 0;
			this.City = "n/a";
			this.WeatherType = "n/a";
		}

		public CurrentWeather(string json)
		{
			this.UpdateCurrentWeather(json);
		}

		public CurrentWeather(string jsonWeather, string jsonLocation)
		{
			this.UpdateCurrentWeather(jsonWeather);
			this.UpdateCurrentCity(jsonLocation);
		}

		public void UpdateCurrentWeather(string json)
		{
			var currentWeather = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

			this.Day = _GetDay();
			this.Temperature = _GetTemperature(currentWeather);
			this.WeatherType = _GetWeatherType(currentWeather);
			//this.City = _GetCity(currentWeather);
		}

		public void UpdateCurrentCity(string json)
		{
			var currentLocation = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
			var resultArray = JsonConvert.DeserializeObject<object[]>(currentLocation["results"].ToString());
			var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultArray[0].ToString());
			var address_components = JsonConvert.DeserializeObject<object[]>(result["address_components"].ToString());
			var address = JsonConvert.DeserializeObject<Dictionary<string, object>>(address_components[2].ToString());
			string city = address["long_name"].ToString();

			this.City = city;
		}

		#region PRIVATE METHODS
		private string _GetWeatherType(Dictionary<string, object> currentWeather)
		{
			var weatherArray = JsonConvert.DeserializeObject<object[]>(currentWeather["weather"].ToString());
			var weather = JsonConvert.DeserializeObject<Dictionary<string, object>>(weatherArray[0].ToString());
			string weatherType = weather["main"].ToString();

			return weatherType;
		}

		//openweathermap = not accurate
		private string _GetCity(Dictionary<string, object> currentWeather)
		{
			return currentWeather["name"].ToString();
		}

		private int _GetTemperature(Dictionary<string, object> currentWeather)
		{
			var main = JsonConvert.DeserializeObject<Dictionary<string, object>>(currentWeather["main"].ToString());
			double temperatureInKelvin = Convert.ToDouble(main["temp"]);

			return Convert.ToInt32(_ConvertKelvinToCelcius(temperatureInKelvin));
		}

		private double _ConvertKelvinToCelcius(double kelvin)
		{
			var kelvinToCelcius = kelvin - 273.15;

			return kelvinToCelcius;
		}

		private string _GetDay()
		{
			DateTime dateToday = DateTime.Today;
			string today = String.Format("{0:dddd, MMMM d, yyyy}", dateToday);

			return today;
		}
		#endregion
	}
}
