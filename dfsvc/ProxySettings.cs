using System.Xml;

namespace System.Deployment.Application.Internal
{
	internal class ProxySettings
	{
		public ProxySettings(string pathToSettingsXml)
		{
			var doc = new XmlDocument();
			doc.Load(pathToSettingsXml);
			var settings = doc.SelectSingleNode("/settings");
			SetProperties(settings);
		}

		private void SetProperties(XmlNode settings)
		{
			UseProxy = bool.Parse(settings.SelectSingleNode("useProxy").InnerText);
			Username = settings.SelectSingleNode("username").InnerText;
			Password = settings.SelectSingleNode("password").InnerText;
			Server = settings.SelectSingleNode("server").InnerText;
		}

		public bool UseProxy { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }
		public string Server { get; private set; }
	}
}