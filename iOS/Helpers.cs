using System;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace FlyLUCK.iOS
{
	public class ModalContentPageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
	{
		// workaround from https://bugzilla.xamarin.com/show_bug.cgi?id=28387

		public ModalContentPageRenderer()
		{
			ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
		}

		public override void WillMoveToParentViewController(UIViewController parent)
		{
			base.WillMoveToParentViewController(parent);

			if (parent != null)
			{
				parent.ModalPresentationStyle = ModalPresentationStyle;
			}
		}
	}
}
