namespace WindowShopping.UI.Live
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Windows.ApplicationModel.Store;

	public class LiveConsumablesListing : ObservableCollection<UnfulfilledConsumable>
	{
		public LiveConsumablesListing()
		{
			StoreContainer.StoreRepository.UnfulfilledConsumableChanged += this.UnfulfilledConsumableChanged;
			this.LoadProducts();
		}

		public void Detach()
		{
			StoreContainer.StoreRepository.UnfulfilledConsumableChanged -= this.UnfulfilledConsumableChanged;
			this.Clear();
		}

		private void UnfulfilledConsumableChanged(object sender, string productId)
		{
			this.LoadProducts();
		}

		public async void LoadProducts()
		{
			this.Clear();

			IReadOnlyList<UnfulfilledConsumable> consumables = await StoreContainer.StoreRepository.GetUnfulfilledConsumablesAsync();

			if (consumables != null)
			{
				foreach (var product in consumables)
				{
					this.Add(product);
				}
			}
		}
	}
}
