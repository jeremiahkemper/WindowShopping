using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WindowShopping.Windows
{
	sealed partial class App : Application
	{
		public App()
		{
#if DEBUG
			// http://msdn.microsoft.com/en-us/library/windows/apps/windows.applicationmodel.store.currentappsimulator.aspx
			// The Current App Simulator must not be in the final application submitted to the store for Approval.
			// Wrapping this in a #if DEBUG allows the developer to play with it during Debug, then during Release, use the real
			// one provided by the Store API.
			StoreContainer.StoreRepository = new CurrentAppSimulatorRepository();
#endif

			this.InitializeComponent();
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			Frame rootFrame = Window.Current.Content as Frame;

			if (rootFrame == null)
			{
				rootFrame = new Frame();
				rootFrame.Language = ApplicationLanguages.Languages[0];
				Window.Current.Content = rootFrame;
			}

			if (rootFrame.Content == null)
			{
				rootFrame.Navigate(typeof(MainPage), e.Arguments);
			}

			Window.Current.Activate();
		}
	}
}
