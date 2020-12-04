﻿using System.Linq;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

namespace Xamarin.CommunityToolkit.Extensions
{
	public static partial class NavigationExtensions
	{
		static void OnShowPopup(BasePopup popup)
		{
			popup.Parent = GetCurrentPage(Application.Current.MainPage);
			Platform.CreateRenderer(popup);

			// https://github.com/xamarin/Xamarin.Forms/blob/0c95d0976cc089fe72476fb037851a64987de83c/Xamarin.Forms.Platform.iOS/PageExtensions.cs#L44
			Page GetCurrentPage(Page currentPage)
			{
				if (currentPage.NavigationProxy.ModalStack.LastOrDefault() is Page modal)
					return modal;
				else if (currentPage is MasterDetailPage mdp)
					return GetCurrentPage(mdp.Detail);
				else if (currentPage is FlyoutPage fp)
					return GetCurrentPage(fp.Detail);
				else if (currentPage is Shell shell && shell.CurrentItem?.CurrentItem is IShellSectionController ssc)
					return ssc.PresentedPage;
				else if (currentPage is IPageContainer<Page> pc)
					return GetCurrentPage(pc.CurrentPage);
				else
					return currentPage;
			}
		}
	}
}