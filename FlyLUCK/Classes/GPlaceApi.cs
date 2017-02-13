using System;
using Newtonsoft.Json;
using System.Net.Http;

namespace FlyLUCK
{
	public class GPlaceApi
	{
		private static string _apikey = "AIzaSyBu5Zz8UWR1Rjr5sqN9s3wllEoOzOSrCIQ";

		private const string BaseUrl = "https://maps.googleapis.com/maps/api/";
		private const string UrlPredictions = "place/autocomplete/json"; // ?input=SEARCHTEXT&key=API_KEY
		private const string UrlDetails = "place/details/json";

		private HttpClient _client;

		public GPlaceApi()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(BaseUrl);
		}

		private string BuildQueryPredictions(string searchText)
		{
			return string.Format("{0}?input={1}&key={2}", UrlPredictions, searchText, _apikey);
		}


		public string GetPredictions(string searchText)
		{

			string predictionString = "";

			var response = _client.GetAsync(BuildQueryPredictions(searchText)).Result;
			if (response.IsSuccessStatusCode)
			{
				var responseContent = response.Content;
				predictionString = responseContent.ReadAsStringAsync().Result;

			}
			return predictionString;
		}
	}
}
