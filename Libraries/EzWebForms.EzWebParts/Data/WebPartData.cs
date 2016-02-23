using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EzWebForms.EzWebParts.Data
{
	[Serializable]
	public sealed class WebPartData
	{
		public WebPartData()
		{
			Properties = new List<WebPartPropertyData>();
		}

		[XmlElement("ID")]
		public string Id { get; set; }

		[XmlElement("ControlPath")]
		public string ControlPath { get; set; }

		[XmlElement("Property")]
		public List<WebPartPropertyData> Properties { get; set; }
	}
}