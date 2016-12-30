using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlyLUCK
{
	public partial class CalendarPage : ContentPage
	{
		private CalendarView _cal;
		public CalendarPage()
		{
			InitializeComponent();
			_cal = new CalendarView();
			sLayout.Children.Add(_cal);
			       
		}
	}
}
