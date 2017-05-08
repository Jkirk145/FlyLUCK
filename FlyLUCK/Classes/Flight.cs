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
		public string FROMSTATE { get; set; }
		public string TOSTATE { get; set; }
		public string FBONAME { get; set; }
		public string FBOADDRESS1 { get; set; }
		public string FBOCITY { get; set; }
		public string FBOSTATE { get; set; }
		public string FBOPHONE { get; set; }
		public string FBOZIP { get; set; }
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

	[Preserve(AllMembers = true)]
	public class Crew
	{
		public string NAME { get; set; }
		public string CELLULAR { get; set; }
	}

	[Preserve(AllMembers = true)]
	public class Passenger
	{
		public string PAXNAME { get; set; }
		public string CELLPHONE { get; set; }
	}

	[Preserve(AllMembers = true)]
	public class User
	{
		public string PAXID { get; set; }
		public string CREWID { get; set; }
		public string NAME { get; set; }
		public string EMAIL { get; set; }
		public string DEPT { get; set; }
		public string USERID { get; set; }

	}
}
