namespace WindowShopping.UI.Core
{
	using Microsoft.Xaml.Interactivity;
	using Windows.UI.Xaml;
	using WindowShopping.Core;

	public abstract class BaseStoreBehavior : DependencyObject, IBehavior
	{
		private DependencyObject associatedObject;

		public abstract void Go();

		public void OnEvent(object sender, object eventArgs)
		{
			this.Go();
		}

		protected ActionCollection GetActionCollection(DependencyProperty collectionProperty)
		{
			ActionCollection actionCollection = (ActionCollection)this.GetValue(collectionProperty);
			if (actionCollection == null)
			{
				actionCollection = new ActionCollection();
				this.SetValue(collectionProperty, actionCollection);
			}

			return actionCollection;
		}

		public DependencyObject AssociatedObject
		{
			get
			{
				return this.associatedObject;
			}
		}

		public void Attach(DependencyObject associatedObject)
		{
			if (associatedObject == this.associatedObject || Windows.ApplicationModel.DesignMode.DesignModeEnabled)
			{
				return;
			}

			this.associatedObject = associatedObject;

			FrameworkElement element = this.associatedObject as FrameworkElement;
			if (element != null)
			{
				element.Loaded += this.OnEvent;
			}

			this.AttachEvents();
		}

		public void Detach()
		{
			FrameworkElement element = this.associatedObject as FrameworkElement;
			if (element != null)
			{
				element.Loaded -= this.OnEvent;
			}

			this.associatedObject = null;

			this.DetachEvents();
		}

		protected abstract void AttachEvents();
		protected abstract void DetachEvents();
	}
}
