namespace WindowShopping.Core
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Windows.ApplicationModel;
	using Windows.ApplicationModel.Store;

	public class CurrentAppRepository : BaseStoreRepository
	{
		protected override LicenseInformation LicenseInformation
		{
			get { return CurrentApp.LicenseInformation; }
		}

		protected override async Task<string> RequestAppPurchaseAsyncInternal()
		{
			return await CurrentApp.RequestAppPurchaseAsync(true);
		}

		protected override async Task<PurchaseResults> RequestProductPurchaseAsyncInternal(string productId)
		{
			return await CurrentApp.RequestProductPurchaseAsync(productId);
		}

		protected override async Task<ListingInformation> LoadListingInformationAsyncInternal()
		{
			return await CurrentApp.LoadListingInformationAsync();
		}

		protected override async Task<IReadOnlyList<UnfulfilledConsumable>> GetUnfulfilledConsumablesAsyncInternal()
		{
			return await CurrentApp.GetUnfulfilledConsumablesAsync();
		}

		protected override async Task<FulfillmentResult> ReportConsumableFulfillmentAsyncInternal(string productId, Guid transactionId)
		{
			return await CurrentApp.ReportConsumableFulfillmentAsync(productId, transactionId);
		}

		protected override Uri GetUriInternal()
		{
			return CurrentApp.LinkUri;
		}
	}
}
