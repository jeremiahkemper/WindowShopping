namespace WindowShopping
{
	using WindowShopping.Core;

	public static class StoreContainer
	{
		private static BaseStoreRepository storeRepository = new CurrentAppRepository();
		public static BaseStoreRepository StoreRepository
		{
			get { return StoreContainer.storeRepository; }
			set { StoreContainer.storeRepository = value; }
		}
	}
}
