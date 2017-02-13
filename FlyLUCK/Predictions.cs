using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FlyLUCK
{
	public class Predictions
	{
		[JsonProperty("predictions")]
		public List<Place> places { get; set;}

		public Predictions()
		{
		}
	}
}
