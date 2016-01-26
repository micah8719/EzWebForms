using System.Collections.Generic;
using System.Web.UI;

namespace EzWebForms.Linq
{
	public static class ControlExtensions
	{
		public static IEnumerable<TControl> FindControlsOfType<TControl>(this Control currentControl)
			where TControl : Control
		{
			foreach (Control control in currentControl.Controls)
			{
				var tControl = control as TControl;

				if (tControl != null)
				{
					yield return tControl;
				}

				foreach (var innerControl in FindControlsOfType<TControl>(control))
				{
					yield return innerControl;
				}
			}
		}
	}
}