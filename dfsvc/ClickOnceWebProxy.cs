using System.Net;

namespace System.Deployment.Application.Internal
{
	internal class ClickOnceWebProxy : IWebProxy
	{
		public ClickOnceWebProxy(ProxySettings settings)
		{
			webProxy = new WebProxy(settings.Server)
			{
				Credentials = new NetworkCredential(settings.Username, settings.Password),
				BypassProxyOnLocal = false
			};
		}

		private readonly WebProxy webProxy;

		public ICredentials Credentials
		{
			get { return webProxy.Credentials; }
			set { }
		}

		public Uri GetProxy(Uri destination)
		{
			return webProxy.GetProxy(destination);
		}

		public bool IsBypassed(Uri host)
		{
			return webProxy.IsBypassed(host);
		}
	}
}