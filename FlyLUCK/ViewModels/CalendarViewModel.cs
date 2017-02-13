using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FlyLUCK.ViewModels
{
	public class CalendarViewModel : INotifyPropertyChanged
	{
		private bool _isBusy;

		public event PropertyChangedEventHandler PropertyChanged;

		public CalendarViewModel()
		{
			_isBusy = false;
		}

		public bool IsLoading
		{
			get
			{
				return _isBusy;
			}
			set
			{
				if (_isBusy != value)
				{
					_isBusy = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this,
							new PropertyChangedEventArgs("IsLoading"));
					}
				}
			}


		}
	}
}
