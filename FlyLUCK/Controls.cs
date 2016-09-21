using System;
using Xamarin.Forms;

namespace FlyLUCK
{
	public class DetailViewLabel : Label
	{
		
	}

	public class CardView : Frame
	{
		private string _origin = "";
		private string _destination = "";
		private DateTime _date = new DateTime();
		private string _time = "";

		public CardView()
		{
		}

		public CardView(string origin, string destination, DateTime deptDate, string deptTime)
		{
			Padding = 0;
			_origin = origin;
			_destination = destination;
			_date = deptDate;
			_time = deptTime;

			StackLayout layout = new StackLayout();

			if (Device.OS == TargetPlatform.iOS)
			{

				Label dateLabel = new Label { Text = "Date: " + _date };
				Label timeLabel = new Label { Text = "Departure Time: " + _time};
				Label citiesLabel = new Label { Text = "From: " + _origin + " To: " + _destination, FontSize = 24 };

				layout.Children.Add(dateLabel);
				layout.Children.Add(citiesLabel);
				layout.Children.Add(timeLabel);
				Content = layout;

				Padding = 20;
				HasShadow = false;
				OutlineColor = Color.Black;
				BackgroundColor = Color.Transparent;
				HorizontalOptions = LayoutOptions.FillAndExpand;

			}
		}


		private void BuildView()
		{
			
		}
	}
}

