// Helpers/Settings.cs
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
		private static readonly string PaxIDDefault = "2";

    #endregion

		public static string UserName
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(UserNameKey, UserNameDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(UserNameKey, value);
			}
		}

    public static string UserID
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(UserIDKey, UserIDDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(UserIDKey, value);
      }
    }

		public static bool FlightCrew
		{
			get
			{
				return AppSettings.GetValueOrDefault<bool>(FlightCrewKey, FlightCrewDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<bool>(FlightCrewKey, value);
			}
		}

		public static string PaxID
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(PaxIDKey, PaxIDDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(PaxIDKey, value);
			}
		}

  }
}