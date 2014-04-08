namespace WindowShopping.UI.Behaviors
{
	using Microsoft.Xaml.Interactivity;
	using Windows.UI.Xaml;
	using WindowShopping.UI.Core;

	public class TrialModeBehavior : BaseStoreBehavior
	{
		public static readonly DependencyProperty TrialApplicationActionsProperty =
			DependencyProperty.Register("TrialApplicationActions", typeof(ActionCollection), typeof(TrialModeBehavior), new PropertyMetadata(null));

		public static readonly DependencyProperty FullApplicationActionsProperty =
			DependencyProperty.Register("FullApplicationActions", typeof(ActionCollection), typeof(TrialModeBehavior), new PropertyMetadata(null));

		public static readonly DependencyProperty ExpiredApplicationActionsProperty =
			DependencyProperty.Register("ExpiredApplicationActions", typeof(ActionCollection), typeof(TrialModeBehavior), new PropertyMetadata(null));

		public ActionCollection TrialApplicationActions
		{
			get { return this.GetActionCollection(TrialModeBehavior.TrialApplicationActionsProperty); }
		}

		public ActionCollection FullApplicationActions
		{
			get { return this.GetActionCollection(TrialModeBehavior.FullApplicationActionsProperty); }
		}

		public ActionCollection ExpiredApplicationActions
		{
			get { return this.GetActionCollection(TrialModeBehavior.ExpiredApplicationActionsProperty); }
		}

		public override void Go()
		{
			ActionCollection actions;

			if (StoreContainer.StoreRepository.IsExpired())
			{
				actions = this.ExpiredApplicationActions;
			}
			else if (StoreContainer.StoreRepository.IsTrial())
			{
				actions = this.TrialApplicationActions;
			}
			else
			{
				actions = this.FullApplicationActions;
			}

			Interaction.ExecuteActions(this.AssociatedObject, actions, this.AssociatedObject);
		}

		protected override void AttachEvents()
		{
			StoreContainer.StoreRepository.AppReceiptReceived += AppReceiptReceived;
		}

		protected override void DetachEvents()
		{
			StoreContainer.StoreRepository.AppReceiptReceived -= AppReceiptReceived;
		}

		private void AppReceiptReceived(object sender, string e)
		{
			this.Go();
		}
	}
}
