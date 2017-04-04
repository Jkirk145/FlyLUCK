using System;
namespace FlyLUCK
{
	public static class Constants
	{
		// URL of REST service
		//public static string ServiceUrl = "https://localhost:8080/FlyLUCKService";
		public static string ServiceUrl = "https://flyluckservice.luckstoneappservices.p.azurewebsites.net/FlyLUCKService";
		// Credentials that are hard coded into the REST service
		public static string Username = "FlyLUCK";
		public static string Password = "Phenom300";

		// Azure app-specific connection string and hub path
		public const string ConnectionString = "<Azure connection string>";
		public const string NotificationHubPath = "<Azure hub path>";
	}
}
