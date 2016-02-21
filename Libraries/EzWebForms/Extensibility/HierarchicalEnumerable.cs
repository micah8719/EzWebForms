using System.Collections.Generic;
using System.Web.UI;

namespace EzWebForms.Extensibility
{
	public class HierarchicalEnumerable<T> : List<T>, IHierarchicalEnumerable
		where T : class, IHierarchyData
	{
		public HierarchicalEnumerable(IEnumerable<T> collection)
		{
			if (collection != null)
			{
				AddRange(collection);
			}
		}

		IHierarchyData IHierarchicalEnumerable.GetHierarchyData(object enumeratedItem)
		{
			return enumeratedItem as T;
		}
	}
}