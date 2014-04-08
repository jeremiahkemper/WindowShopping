namespace WindowShopping.UI.Behaviors
{
	using Microsoft.Xaml.Interactivity;
	using Windows.UI.Xaml;

	public class PurchaseFullAppAction : DependencyObject, IAction
	{
		public object Execute(object sender, object parameter)
		{
			StoreContainer.StoreRepository.RequestAppPurchaseAsync();
			return true;
		}
	}
}
