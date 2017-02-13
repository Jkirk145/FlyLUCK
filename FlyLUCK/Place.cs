using System;
using Newtonsoft.Json;

namespace FlyLUCK
{
	public class Place
	{
		[JsonProperty("description")]
		public string description { get; set;}

		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("place_id")]
		public string place_id { get; set;}

		[JsonProperty("reference")]
		public string reference { get; set;}

		public Place()
		{
		}
	}
}
