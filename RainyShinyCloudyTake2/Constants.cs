using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RainyShinyCloudyTake2
{
	public static class Constants
	{
		private static string WEATHER_API_KEY = "09c06bc14124036990128caa27b90868";
		public static string CURRENT_WEATHER_URL = "";
		public static string FORECAST_URL = "";

		public static void CONSTRUCT_WEATHER_URL(double latitude, double longitude)
		{
			CURRENT_WEATHER_URL = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={WEATHER_API_KEY}";
			FORECAST_URL = $"http://api.openweathermap.org/data/2.5/forecast/daily?lat={latitude}&lon={longitude}&cnt=10&mode=json&appid={WEATHER_API_KEY}";
		}

		private static string LOCATION_API_KEY = "AIzaSyAdZ-vd9-gzfT5r8qDUKnmP_2-kom__kb8";
		public static string LOCATION_URL = "";

		public static void CONSTRUCT_LOCATION_URL(double latitude, double longitude)
		{
			LOCATION_URL = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={LOCATION_API_KEY}";
		}
	}
}