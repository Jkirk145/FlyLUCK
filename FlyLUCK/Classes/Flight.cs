using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace FlyLUCK
{

	[Preserve(AllMembers = true)]
	public class Flight
	{
		public int LEGID { get; set; }
		public int TRIPNUM { get; set; }
		public string LOCALLEAVE { get; set; }
		public string ORIGIN { get; set; }
		public string FROMAIRPORTNAME { get; set; }
		public string DEST { get; set; }
		public string TOAIRPORTNAME { get; set; }


		public Flight() { }

	}
}
