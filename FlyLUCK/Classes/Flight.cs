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
		public string LOCALARRIVE { get; set; }
		public string ORIGIN { get; set; }
		public string FROMAIRPORTNAME { get; set; }
		public string DEST { get; set; }
		public string TOAIRPORTNAME { get; set; }
		public string FROMCITY { get; set; }
		public string TOCITY { get; set; }


		public Flight() { }

	}

	[Preserve(AllMembers = true)]
	public class Request
	{
		public string Subject { get; set;}
		public string Destination { get; set; }
		public string DateDepart { get; set;}
		public string TimeDepart { get; set; }
		public string DateReturn { get; set;}
		public string TimeReturn { get; set;}
		public string Requestor { get; set; }
		public string NumPax { get; set;}
		public string RentalCar { get; set;}
		public string Specials { get; set;}
		public string Purpose { get; set;}
	}
}
