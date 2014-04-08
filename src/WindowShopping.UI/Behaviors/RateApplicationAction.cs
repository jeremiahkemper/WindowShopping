namespace WindowShopping.UI.Behaviors
{
	using Microsoft.Xaml.Interactivity;
	using Windows.UI.Xaml;
	using WindowShopping;

	public class RateApplicationAction : DependencyObject, IAction
	{
		public object Execute(object sender, object parameter)
		{
			StoreContainer.StoreRepository.LaunchApplicationRatingView();
			return true;
		}
	}
}
