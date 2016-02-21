using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace EzWebForms.Extensibility
{
	public class HierarchicalData<T> : IHierarchyData
		where T : class, IHierarchyData
	{
		public readonly T Parent;
		public readonly HierarchicalEnumerable<T> Children;

		protected HierarchicalData(T parent, IEnumerable<T> children)
		{
			Parent = parent;
			Children = new HierarchicalEnumerable<T>(children);
		}

		IHierarchicalEnumerable IHierarchyData.GetChildren()
		{
			return Children;
		}

		IHierarchyData IHierarchyData.GetParent()
		{
			return Parent;
		}

		bool IHierarchyData.HasChildren => Children != null && Children.Any();

		string IHierarchyData.Path => Path;

		object IHierarchyData.Item => this;

		string IHierarchyData.Type => Type;

		public virtual string Name => String.Empty;
		public virtual string Path => String.Empty;
		public virtual string Type => String.Empty;
	}
}