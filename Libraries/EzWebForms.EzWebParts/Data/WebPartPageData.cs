using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using EzWebForms.EzWebParts.Base;

namespace EzWebForms.EzWebParts.Data
{
	[Serializable]
	public sealed class WebPartPageData : XmlObjectBase
	{
		public WebPartPageData()
		{
			WebPartZones = new List<WebPartZoneData>();
		}

		[XmlElement("WebPartZone")]
		public List<WebPartZoneData> WebPartZones { get; set; }
	}
}