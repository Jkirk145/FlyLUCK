using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlyLUCK
{
	public partial class FlightList : ContentPage
	{
		

		public FlightList()
		{
			InitializeComponent();

			CardView cv1 = new CardView("OFP", "JYO", new DateTime(2016, 09, 27), "08:00 AM");
			CardView cv2 = new CardView("JYO", "SPA", new DateTime(2016, 09, 27), "09:00 AM");

			var tapped = new TapGestureRecognizer();
			tapped.Tapped += (s, e) =>
			{
				OnTapped(s, e);
			};


			cv1.GestureRecognizers.Add(tapped);
			cv2.GestureRecognizers.Add(tapped);

			layout.Children.Add(cv1);
			layout.Children.Add(cv2);


		}

		async void OnTapped(Object sender, EventArgs e)
		{
			//DisplayAlert("Alert!", "Something happened", "Bail!");
			await Navigation.PushModalAsync(new FlightDetail());
		}
	}
}

