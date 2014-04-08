namespace WindowShopping.Core
{
	using System;

	public class StoreException : Exception
	{
		public Exception BaseException { get; set; }
		public string Description { get; set; }
		public string[] Args { get; set; }

		public StoreException(string description, Exception baseException)
		{
			this.Description = description;
			this.BaseException = baseException;
		}

		public StoreException(string description, Exception baseException, string[] args)
			: this(description, baseException)
		{
			this.Args = args;
		}

		public override Exception GetBaseException()
		{
			return base.GetBaseException();
		}
	}
}
