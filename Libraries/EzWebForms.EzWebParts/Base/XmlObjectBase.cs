using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace EzWebForms.EzWebParts.Base
{
	[Serializable]
	public abstract class XmlObjectBase
	{
		public string Serialize()
		{
			var stringBuilder = new StringBuilder();
			var serializer = new XmlSerializer(GetType());
			using (var textWriter = new StringWriter(stringBuilder))
			{
				try
				{
					serializer.Serialize(textWriter, this);
					return stringBuilder.ToString();
				}
				catch
				{
					return string.Empty;
				}
			}
		}

		public static TXmlObject Deserialize<TXmlObject>(string xml)
			where TXmlObject : XmlObjectBase, new()
		{
			var serializer = new XmlSerializer(typeof (TXmlObject));
			using (var textReader = new StringReader(xml))
			{
				try
				{
					return serializer.Deserialize(textReader) as TXmlObject ?? new TXmlObject();
				}
				catch
				{
					return new TXmlObject();
				}
			}
		}
	}
}