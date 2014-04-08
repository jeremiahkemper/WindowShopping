namespace WindowShopping.UI.Live
{
	using System;
	using System.ComponentModel;

	public class LiveAppInfo : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private bool isTrial;
		public bool IsTrial
		{
			get { return this.isTrial; }
			set
			{
				if (this.isTrial != value)
				{
					this.isTrial = value;
					this.OnPropertyChanged("IsTrial");
					this.OnPropertyChanged("IsFullVersion");
				}
			}
		}

		public bool IsFullVersion
		{
			get { return !this.IsTrial; }
		}

		public LiveAppInfo()
		{
			StoreContainer.StoreRepository.LicenseChanged += this.StoreRepository_LicenseChanged;
			StoreContainer.StoreRepository.AppReceiptReceived += this.StoreRepository_AppReceiptReceived;

			this.Sync();
		}

		public void Detach()
		{
			StoreContainer.StoreRepository.LicenseChanged -= this.StoreRepository_LicenseChanged;
			StoreContainer.StoreRepository.AppReceiptReceived -= this.StoreRepository_AppReceiptReceived;
		}

		private void StoreRepository_AppReceiptReceived(object sender, string e)
		{
			this.Sync();
		}

		private void StoreRepository_LicenseChanged(object sender, EventArgs e)
		{
			this.Sync();
		}

		private void Sync()
		{
			this.IsTrial = StoreContainer.StoreRepository.IsTrial();
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
