using System.IO;
using System.Net;
using System.Reflection;

namespace System.Deployment.Application.Internal
{
	internal static class EntryPoint
	{
		[MTAThread]
		public static int Main(string[] args)
		{
			var settings = GetProxySettings();
			if(settings.UseProxy)
				SetClickOnceProxy(settings);

			LoadDFServiceEntryPoint(args);
			return 0;
		}

		private static void SetClickOnceProxy(ProxySettings settings)
		{
			ServicePointManager.Expect100Continue = false;
			WebRequest.DefaultWebProxy = new ClickOnceWebProxy(settings);
		}

		private static ProxySettings GetProxySettings()
		{
			string settingsPath = GetProxySettingsPath();
			return new ProxySettings(settingsPath);
		}

		private static string GetProxySettingsPath()
		{
			string winDir = Environment.GetEnvironmentVariable("windir");
			string dotNetBasePath = Path.Combine(winDir, "Microsoft.NET");
			return Path.Combine(dotNetBasePath, "ClickOnceProxySettings.xml");
		}

		private static void LoadDFServiceEntryPoint(string[] parameters)
		{
#if NET2
			var assembly = Assembly.Load("System.Deployment, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
#else
			var assembly = Assembly.Load("System.Deployment, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
#endif
			var entryPoint = assembly.GetType("System.Deployment.Application.DFServiceEntryPoint").GetMethod("Initialize");
			entryPoint.Invoke(null, new object[] { parameters });
		}
	}
}