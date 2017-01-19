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
		public static string URL = "";

		public static string API_KEY = "09c06bc14124036990128caa27b90868";

		public static void ConstructURL(double latitude, double longitude)
		{
			URL = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={API_KEY}";
		}
	}
}