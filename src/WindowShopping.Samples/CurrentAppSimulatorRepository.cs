namespace WindowShopping.Samples
{
#if DEBUG
	// http://msdn.microsoft.com/en-us/library/windows/apps/windows.applicationmodel.store.currentappsimulator.aspx
	// The Current App Simulator must not be in the final application submitted to the store for Approval.
	// Wrapping this in a #if DEBUG allows the developer to play with it during Debug, then during Release, use the real
	// one provided by the Store API.

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Windows.ApplicationModel;
	using Windows.ApplicationModel.Store;
	using Windows.Storage;
	using WindowShopping.Core;

	public class CurrentAppSimulatorRepository : BaseStoreRepository
	{
		public CurrentAppSimulatorRepository()
			: base()
		{
			this.Initialize();
		}

		private async void Initialize()
		{
			StorageFolder proxyDataFolder = await Package.Current.InstalledLocation.GetFolderAsync("data");
			StorageFile proxyFile = await proxyDataFolder.GetFileAsync("simulated-products.xml");
			await CurrentAppSimulator.ReloadSimulatorAsync(proxyFile);
		}

		override protected LicenseInformation LicenseInformation
		{
			get { return CurrentAppSimulator.LicenseInformation; }
		}

		protected override async Task<string> RequestAppPurchaseAsyncInternal()
		{
			return await CurrentAppSimulator.RequestAppPurchaseAsync(true);
		}

		protected override async Task<PurchaseResults> RequestProductPurchaseAsyncInternal(string productId)
		{
			return await CurrentAppSimulator.RequestProductPurchaseAsync(productId);
		}

		protected override async Task<ListingInformation> LoadListingInformationAsyncInternal()
		{
			return await CurrentAppSimulator.LoadListingInformationAsync();
		}

		protected override async Task<IReadOnlyList<UnfulfilledConsumable>> GetUnfulfilledConsumablesAsyncInternal()
		{
			return await CurrentAppSimulator.GetUnfulfilledConsumablesAsync();
		}

		protected override async Task<FulfillmentResult> ReportConsumableFulfillmentAsyncInternal(string productId, Guid transactionId)
		{
			return await CurrentAppSimulator.ReportConsumableFulfillmentAsync(productId, transactionId);
		}

		protected override Uri GetUriInternal()
		{
			return CurrentAppSimulator.LinkUri;
		}
	}
#endif
}
