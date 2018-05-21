// Helpers/Settings.cs
using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace FlyLUCK.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

		#region Setting Constants

		private const string UserNameKey = "username_key";
		private static readonly string UserNameDefault = string.Empty;

    	private const string UserIDKey = "userid_key";
    	private static readonly string UserIDDefault = string.Empty;

		private const string FlightCrewKey = "flightcrew_key";
		private static readonly bool FlightCrewDefault = false;

		private const string PaxIDKey = "paxid_key";
		private static readonly string PaxIDDefault = string.Empty;

		private const string AccessTokenKey = "accesstoken_key";
		private static readonly string AccessTokenDefault = string.Empty;

		private const string SessionExpiresKey = "sessionexpires_key";
		private static readonly DateTime SessionExpiresDefault = new DateTime(1990, 12, 31);

		private const string AdminKey = "admin_key";
		private static readonly bool AdminDefault = false;


    	#endregion

		public static DateTime SessionExpires
		{
			get
			{
				//return AppSettings.GetValueOrDefault<DateTime>(SessionExpiresKey, SessionExpiresDefault);
				return AppSettings.GetValueOrDefault(SessionExpiresKey, SessionExpiresDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SessionExpiresKey, value);
			}
		}

		public static string AccessToken
		{
			get
			{
				return AppSettings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault);
			}
			set 
			{
				AppSettings.AddOrUpdateValue(AccessTokenKey, value);
			}
		}

		public static string UserName
		{
			get
			{
				return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(UserNameKey, value);
			}
		}

	    public static string UserID
	    {
	      get
	      {
	        return AppSettings.GetValueOrDefault(UserIDKey, UserIDDefault);
	      }
	      set
	      {
	        AppSettings.AddOrUpdateValue(UserIDKey, value);
	      }
	    }

		public static bool FlightCrew
		{
			get
			{
				return AppSettings.GetValueOrDefault(FlightCrewKey, FlightCrewDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(FlightCrewKey, value);
			}
		}

		public static bool IsAdmin
		{
			get
			{
				return AppSettings.GetValueOrDefault(AdminKey, AdminDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(AdminKey, value);
			}
		}

		public static string PaxID
		{
			get
			{
				return AppSettings.GetValueOrDefault(PaxIDKey, PaxIDDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(PaxIDKey, value);
			}
		}

  }
}