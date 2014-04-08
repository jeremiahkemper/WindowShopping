namespace WindowShopping.UI.Behaviors
{
	using Microsoft.Xaml.Interactivity;
	using Windows.UI.Xaml;
	using WindowShopping.UI.Core;
	using WindowShopping.Core;

	public class ProductStatusBehavior : BaseStoreBehavior
	{
		public static readonly DependencyProperty ProductIdProperty =
			DependencyProperty.Register("ProductId", typeof(string), typeof(ProductStatusBehavior), new PropertyMetadata(null));

		public static readonly DependencyProperty PurchasedActionsProperty =
			DependencyProperty.Register("PurchasedActions", typeof(ActionCollection), typeof(ProductStatusBehavior), new PropertyMetadata(null));

		public static readonly DependencyProperty NotPurchasedActionsProperty =
			DependencyProperty.Register("NotPurchasedActions", typeof(ActionCollection), typeof(ProductStatusBehavior), new PropertyMetadata(null));

		public string ProductId
		{
			get { return (string)this.GetValue(ProductStatusBehavior.ProductIdProperty); }
			set { this.SetValue(ProductStatusBehavior.ProductIdProperty, value); }
		}

		public ActionCollection PurchasedActions
		{
			get { return this.GetActionCollection(ProductStatusBehavior.PurchasedActionsProperty); }
		}

		public ActionCollection NotPurchasedActions
		{
			get { return this.GetActionCollection(ProductStatusBehavior.NotPurchasedActionsProperty); }
		}

		public override void Go()
		{
			ActionCollection actions = (StoreContainer.StoreRepository.IsProductIdPurchased(this.ProductId)) ? this.PurchasedActions : this.NotPurchasedActions;

			Interaction.ExecuteActions(this.AssociatedObject, actions, this.AssociatedObject);
		}

		protected override void AttachEvents()
		{
			StoreContainer.StoreRepository.ItemReceiptReceived += ItemReceiptReceived;
		}

		protected override void DetachEvents()
		{
			StoreContainer.StoreRepository.ItemReceiptReceived -= ItemReceiptReceived;
		}

		private void ItemReceiptReceived(object sender, ProductReceiptInfo e)
		{
			if (e.ProductId == this.ProductId)
			{
				this.Go();
			}
		}
	}
}
