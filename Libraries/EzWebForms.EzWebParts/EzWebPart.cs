using System;
using System.Linq;
using System.Web.UI;
using EzWebForms.Controls;
using EzWebForms.EzWebParts.Data;

namespace EzWebForms.EzWebParts
{
	public class EzWebPart : EzUserControl
	{
		public EzWebPart()
		{
			Init += EzWebPart_Init;
		}

		public new EzWebPartPage Page => base.Page as EzWebPartPage;

		public WebPartData Data
		{
			get
			{
				if (Page == null)
				{
					return new WebPartData {Id = ID};
				}

				var zone = GetParentZone(this);

				if (zone == null)
				{
					return new WebPartData {Id = ID};
				}

				var webPart = zone.WebParts.FirstOrDefault(part => part.Id == ID);

				return webPart ?? new WebPartData {Id = ID};
			}
		}

		private void EzWebPart_Init(object sender, EventArgs e)
		{
			// todo: Set web part properties based on Data object
		}

		private static WebPartZoneData GetParentZone(Control currentControl)
		{
			while (true)
			{
				if (currentControl == null)
				{
					return null;
				}

				var zone = currentControl as EzWebPartZone;

				if (zone != null)
				{
					return zone.Data;
				}

				currentControl = currentControl.Parent;
			}
		}
	}
}