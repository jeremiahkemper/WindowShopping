namespace WindowShopping.UI.Live
{
	using System.Collections.ObjectModel;
	using Windows.ApplicationModel.Store;

	public class LiveProductListing : ObservableCollection<ProductListing>
	{
		public LiveProductListing()
		{
			this.LoadProducts();
		}

		public async void LoadProducts()
		{
			this.Clear();

			ListingInformation listingInformation = await StoreContainer.StoreRepository.LoadListingInformationAsync();

			if (listingInformation != null)
			{
				foreach (var product in listingInformation.ProductListings.Values)
				{
					this.Add(product);
				}
			}
		}
	}
}
