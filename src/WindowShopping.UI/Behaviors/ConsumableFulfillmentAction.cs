namespace WindowShopping.UI.Behaviors
{
	using Microsoft.Xaml.Interactivity;
	using System;
	using System.Threading.Tasks;
	using Windows.UI.Xaml;

	public class ConsumableFulfillmentAction : DependencyObject, IAction
	{
		public static readonly DependencyProperty ProductIdProperty =
			DependencyProperty.Register("ProductId", typeof(string), typeof(ConsumableFulfillmentAction), new PropertyMetadata(true));

		public string ProductId
		{
			get { return (string)this.GetValue(ConsumableFulfillmentAction.ProductIdProperty); }
			set { this.SetValue(ConsumableFulfillmentAction.ProductIdProperty, value); }
		}

		public static readonly DependencyProperty TransactionIdProperty =
			DependencyProperty.Register("TransactionId", typeof(Guid), typeof(ConsumableFulfillmentAction), new PropertyMetadata(true));

		public Guid TransactionId
		{
			get { return (Guid)this.GetValue(ConsumableFulfillmentAction.TransactionIdProperty); }
			set { this.SetValue(ConsumableFulfillmentAction.TransactionIdProperty, value); }
		}

		public object Execute(object sender, object parameter)
		{
			StoreContainer.StoreRepository.ReportConsumableFulfillmentAsync(this.ProductId, this.TransactionId);

			return true;
		}
	}
}
