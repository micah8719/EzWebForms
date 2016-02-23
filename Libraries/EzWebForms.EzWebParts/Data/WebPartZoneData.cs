using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EzWebForms.EzWebParts.Data
{
	[Serializable]
	public sealed class WebPartZoneData
	{
		public WebPartZoneData()
		{
			WebParts = new List<WebPartData>();
			Properties = new List<WebPartPropertyData>();
		}

		[XmlElement("ID")]
		public string Id { get; set; }

		[XmlElement("WebPart")]
		public List<WebPartData> WebParts { get; set; }

		[XmlElement("Property")]
		public List<WebPartPropertyData> Properties { get; set; }
	}
}