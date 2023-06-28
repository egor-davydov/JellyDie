namespace Facebook.Unity
{
	internal interface IFacebookCallbackHandler
	{
		void OnInitComplete(string message);

		void OnLoginComplete(string message);

		void OnAppRequestsComplete(string message);

		void OnShareLinkComplete(string message);
	}
}
