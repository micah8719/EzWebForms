using System;
using System.Xml.Serialization;

namespace EzWebForms.EzWebParts.Data
{
	[Serializable]
	public sealed class WebPartPropertyData
	{
		[XmlElement("Name")]
		public string Name { get; set; }

		[XmlElement("Value")]
		public string Value { get; set; }
	}
}