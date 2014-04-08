namespace WindowShopping.UI.Behaviors
{
	using Microsoft.Xaml.Interactivity;
	using Windows.UI.Xaml;

	public class PurchaseProductAction : DependencyObject, IAction
	{
		public static readonly DependencyProperty ProductIdProperty =
			DependencyProperty.Register("ProductId", typeof(string), typeof(PurchaseProductAction), new PropertyMetadata(true));

		public string ProductId
		{
			get { return (string)this.GetValue(PurchaseProductAction.ProductIdProperty); }
			set { this.SetValue(PurchaseProductAction.ProductIdProperty, value); }
		}

		public object Execute(object sender, object parameter)
		{
			StoreContainer.StoreRepository.RequestProductPurchaseAsync(this.ProductId);

			return true;
		}
	}
}
