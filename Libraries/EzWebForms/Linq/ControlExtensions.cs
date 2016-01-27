using System;
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

		public static bool IsInUpdatePanel(this Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException(nameof(control));
			}

			var currentParent = control.Parent;

			while (currentParent != null && !(currentParent is Page))
			{
				if (currentParent is UpdatePanel)
				{
					return true;
				}

				currentParent = currentParent.Parent;
			}

			return false;
		}
	}
}