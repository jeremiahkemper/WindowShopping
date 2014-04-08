namespace WindowShopping.Core
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Windows.ApplicationModel;
	using Windows.ApplicationModel.Store;
	using WindowShopping.Core;

	public abstract class BaseStoreRepository
	{
		public event EventHandler LicenseChanged;
		public event EventHandler<ProductReceiptInfo> ItemReceiptReceived;
		public event EventHandler<string> AppReceiptReceived;
		public event EventHandler<string> UnfulfilledConsumableChanged;
		public event EventHandler<StoreException> ExceptionReceived;

		public BaseStoreRepository()
		{

		}

		public async void RequestAppPurchaseAsync()
		{
			try
			{
				if (this.IsTrial())
				{
					string receipt = await this.RequestAppPurchaseAsyncInternal();

					this.OnLicenseChanged();

					this.BroadcastAppReceipt(receipt);
				}
			}
			catch(Exception exception)
			{
				this.BroadcastException(new StoreException("RequestAppPurchaseAsync", exception));
			}
		}

		public bool IsTrial()
		{
			bool result = true;

			try
			{
				result = this.LicenseInformation.IsTrial;
			}
			catch (Exception exception)
			{
				this.BroadcastException(new StoreException("IsTrial", exception));
			}

			return result;
		}

		public bool IsExpired()
		{
			bool result = false;

			try
			{
				result = this.LicenseInformation.IsTrial &&
						 this.LicenseInformation.ExpirationDate <= DateTime.Now;
			}
			catch (Exception exception)
			{
				this.BroadcastException(new StoreException("IsExpired", exception));
			}

			return result;
		}

		public bool IsProductIdPurchased(string productId)
		{
			return string.IsNullOrEmpty(productId) == false &&
					this.LicenseInformation.ProductLicenses.ContainsKey(productId) == true &&
					this.LicenseInformation.ProductLicenses[productId].IsActive;
		}

		public async void RequestProductPurchaseAsync(string productId)
		{
			try
			{
				if (this.IsProductIdPurchased(productId) == false)
				{
					PurchaseResults results = await this.RequestProductPurchaseAsyncInternal(productId);

					if (results.Status == ProductPurchaseStatus.Succeeded)
					{
						this.BroadcastItemReceipt(productId, results.ReceiptXml);
						this.BroadcastUnfulfilledConsumableChanged(productId);
					}
				}
			}
			catch(Exception exception)
			{
				this.BroadcastException(new StoreException("RequestProductPurchaseAsync", exception, new string[] { productId }));
			}
		}

		public async Task<ListingInformation> LoadListingInformationAsync()
		{
			ListingInformation listingInformation = null;

			try
			{
				listingInformation = await this.LoadListingInformationAsyncInternal();
			}
			catch(Exception exception)
			{
				this.BroadcastException(new StoreException("LoadListingInformation", exception));
			}

			return listingInformation;
		}

		public async Task<IReadOnlyList<UnfulfilledConsumable>> GetUnfulfilledConsumablesAsync()
		{
			return await this.GetUnfulfilledConsumablesAsyncInternal();
		}

		public async void ReportConsumableFulfillmentAsync(string productId, Guid transactionId)
		{
			try
			{
				FulfillmentResult result = await this.ReportConsumableFulfillmentAsyncInternal(productId, transactionId);

				this.BroadcastUnfulfilledConsumableChanged(productId);
			}
			catch(Exception exception)
			{
				this.BroadcastException(new StoreException("ReportConsumableFulfillmentAsync", exception));
			}
		}

		public async void LaunchApplicationRatingView()
		{
			Uri linkUri = this.GetUriInternal();
			await Windows.System.Launcher.LaunchUriAsync(linkUri);
		}

		private void OnLicenseChanged()
		{
			if (this.LicenseChanged != null)
			{
				this.LicenseChanged(this, new EventArgs());
			}
		}
		private void BroadcastItemReceipt(string productId, string receipt)
		{
			if (this.ItemReceiptReceived != null && string.IsNullOrEmpty(receipt) == false && string.IsNullOrEmpty(receipt) == false)
			{
				this.ItemReceiptReceived(this, new ProductReceiptInfo() { ProductId = productId, Receipt = receipt });
			}
		}

		private void BroadcastAppReceipt(string receipt)
		{
			if (this.AppReceiptReceived != null && string.IsNullOrEmpty(receipt) == false)
			{
				this.AppReceiptReceived(this, receipt);
			}
		}

		private void BroadcastUnfulfilledConsumableChanged(string productId)
		{
			if (this.UnfulfilledConsumableChanged != null && string.IsNullOrEmpty(productId) == false)
			{
				this.UnfulfilledConsumableChanged(this, productId);
			}
		}

		private void BroadcastException(StoreException exception)
		{
			if (this.ExceptionReceived != null && exception != null)
			{
				this.ExceptionReceived(this, exception);
			}
		}

		protected abstract LicenseInformation LicenseInformation { get; }
		protected abstract Task<string> RequestAppPurchaseAsyncInternal();
		protected abstract Task<PurchaseResults> RequestProductPurchaseAsyncInternal(string productId);
		protected abstract Task<ListingInformation> LoadListingInformationAsyncInternal();
		protected abstract Task<IReadOnlyList<UnfulfilledConsumable>> GetUnfulfilledConsumablesAsyncInternal();
		protected abstract Task<FulfillmentResult> ReportConsumableFulfillmentAsyncInternal(string productId, Guid transactionId);
		protected abstract Uri GetUriInternal();
	}
}
