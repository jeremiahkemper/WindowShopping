using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WindowShopping.Core;

namespace WindowShopping.Windows
{
	public sealed partial class MainPage : Page
	{
		public ObservableCollection<string> Log { get; private set; }

		public MainPage()
		{
			this.Log = new ObservableCollection<string>();

			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			StoreContainer.StoreRepository.AppReceiptReceived += this.StoreRepository_AppReceiptReceived;
			StoreContainer.StoreRepository.ExceptionReceived += this.StoreRepository_ExceptionReceived;
			StoreContainer.StoreRepository.ItemReceiptReceived += this.StoreRepository_ItemReceiptReceived;
			StoreContainer.StoreRepository.LicenseChanged += this.StoreRepository_LicenseChanged;
			StoreContainer.StoreRepository.UnfulfilledConsumableChanged += this.StoreRepository_UnfulfilledConsumableChanged;

			base.OnNavigatedTo(e);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			StoreContainer.StoreRepository.AppReceiptReceived -= this.StoreRepository_AppReceiptReceived;
			StoreContainer.StoreRepository.ExceptionReceived -= this.StoreRepository_ExceptionReceived;
			StoreContainer.StoreRepository.ItemReceiptReceived -= this.StoreRepository_ItemReceiptReceived;
			StoreContainer.StoreRepository.LicenseChanged -= this.StoreRepository_LicenseChanged;
			StoreContainer.StoreRepository.UnfulfilledConsumableChanged -= this.StoreRepository_UnfulfilledConsumableChanged;

			base.OnNavigatedFrom(e);
		}

		private void LogMessage(string message)
		{
			this.Log.Insert(0, message);
		}

		private void StoreRepository_LicenseChanged(object sender, EventArgs e)
		{
			this.LogMessage("LicenseChanged");
		}

		private void StoreRepository_ItemReceiptReceived(object sender, ProductReceiptInfo e)
		{
			this.LogMessage("ItemReceiptReceived: " + e.ProductId + " :: " + e.Receipt);
		}

		private void StoreRepository_ExceptionReceived(object sender, StoreException e)
		{
			this.LogMessage("ExceptionReceived: " + e.Description + " :: " + e.BaseException.Message);
		}

		private void StoreRepository_AppReceiptReceived(object sender, string e)
		{
			this.LogMessage("AppReceiptReceived: " + e);
		}

		private void StoreRepository_UnfulfilledConsumableChanged(object sender, string e)
		{
			this.LogMessage("UnfulfilledConsumableChanged: " + e);
		}
	}
}