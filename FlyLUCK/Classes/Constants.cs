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
		public const string ConnectionString = "Endpoint=sb://flyluckns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=CsN55kouxy4zT/O1NvQbkI0pRRp0/TBi8TyEYdZRAPE=";
		public const string NotificationHubPath = "flyluckhub";
	}
}
