using System;
using System.Linq;
using EzWebForms.Controls;
using EzWebForms.EzWebParts.Data;

namespace EzWebForms.EzWebParts
{
	public class EzWebPartZone : EzWebControl
	{
		public EzWebPartZone()
		{
			Init += EzWebPartZone_Init;
		}

		public new EzWebPartPage Page => base.Page as EzWebPartPage;

		public WebPartZoneData Data
		{
			get
			{
				if (Page == null)
				{
					return new WebPartZoneData {Id = ID};
				}

				var data = Page.Data.WebPartZones.FirstOrDefault(zone => zone.Id == ID);

				return data ?? new WebPartZoneData {Id = ID};
			}
		}

		private void EzWebPartZone_Init(object sender, EventArgs e)
		{
			foreach (var webPart in Data.WebParts.Select(LoadWebPart).Where(webPart => webPart != null))
			{
				Controls.Add(webPart);
			}
		}

		private EzWebPart LoadWebPart(WebPartData data)
		{
			try
			{
				var webPart = Page.LoadControl(data.ControlPath) as EzWebPart;

				if (webPart == null)
				{
					return null;
				}

				webPart.ID = data.Id;

				return webPart;
			}
			catch
			{
				return null;
			}
		}
	}
}