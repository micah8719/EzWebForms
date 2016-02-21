using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EzWebForms.Linq;

namespace EzWebForms.Controls
{
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class EzHierarchicalRepeater : EzDataBoundControl
	{
		private static readonly object ItemCreatedEvent = new object();
		private static readonly object ItemDataBoundEvent = new object();
		private static readonly object ItemCommandEvent = new object();

		[TemplateContainer(typeof (RepeaterItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate HeaderTemplate { get; set; }

		[TemplateContainer(typeof (RepeaterItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate ItemTemplate { get; set; }

		[TemplateContainer(typeof (RepeaterItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate FooterTemplate { get; set; }

		[DefaultValue("ItemPlaceholder")]
		public virtual string ItemPlaceholderId
		{
			get { return (string) ViewState["ItemPlaceholderID"] ?? "ItemPlaceholder"; }
			set { ViewState["ItemPlaceholderID"] = value; }
		}

		public event RepeaterItemEventHandler ItemCreated
		{
			add { Events.AddHandler(ItemCreatedEvent, value); }
			remove { Events.RemoveHandler(ItemCreatedEvent, value); }
		}

		public event RepeaterItemEventHandler ItemDataBound
		{
			add { Events.AddHandler(ItemDataBoundEvent, value); }
			remove { Events.RemoveHandler(ItemDataBoundEvent, value); }
		}

		public event RepeaterCommandEventHandler ItemCommand
		{
			add { Events.AddHandler(ItemCommandEvent, value); }
			remove { Events.RemoveHandler(ItemCommandEvent, value); }
		}

		protected virtual void OnItemCommand(RepeaterCommandEventArgs e)
		{
			var handler = (RepeaterCommandEventHandler) Events[ItemCommandEvent];
			handler?.Invoke(this, e);
		}

		protected virtual void OnItemCreated(RepeaterItemEventArgs e)
		{
			var handler = (RepeaterItemEventHandler) Events[ItemCreatedEvent];
			handler?.Invoke(this, e);
		}

		protected virtual void OnItemDataBound(RepeaterItemEventArgs e)
		{
			var handler = (RepeaterItemEventHandler) Events[ItemDataBoundEvent];
			handler?.Invoke(this, e);
		}

		protected override int CreateChildControlsImpl(IEnumerable dataSource, bool dataBinding, ref int controlCount)
		{
			if (!(dataSource is IHierarchicalEnumerable))
			{
				return controlCount;
			}

			var enumerator = dataSource.GetEnumerator();

			while (enumerator.MoveNext())
			{
				var node = (IHierarchyData) enumerator.Current;

				CreateHierarchy(this, node, dataBinding, ref controlCount);
			}

			return controlCount;
		}

		private void CreateHierarchy(Control currentControl, IHierarchyData node, bool dataBinding, ref int controlCount)
		{
			var createHeader = HeaderTemplate != null;
			var createFooter = FooterTemplate != null;
			var item = CreateItem(controlCount, ListItemType.Item, dataBinding, node);

			if (createHeader)
			{
				currentControl.Controls.Add(CreateItem(-1, ListItemType.Header, false, null));
			}

			currentControl.Controls.Add(item);

			if (createFooter)
			{
				currentControl.Controls.Add(CreateItem(-1, ListItemType.Footer, false, null));
			}

			var itemPlaceHolder = item.FindControl(ItemPlaceholderId) ??
			                      item.FindControlsOfType<PlaceHolder>()
				                      .FirstOrDefault(control => control.ID == ItemPlaceholderId);

			++controlCount;

			foreach (IHierarchyData child in node.GetChildren())
			{
				CreateHierarchy(itemPlaceHolder, child, dataBinding, ref controlCount);
			}
		}

		private RepeaterItem CreateItem(int itemIndex, ListItemType itemType, bool dataBind, object dataItem)
		{
			var repeaterItem = CreateItem(itemIndex, itemType);
			var e = new RepeaterItemEventArgs(repeaterItem);

			InitializeItem(repeaterItem);

			if (dataBind)
			{
				repeaterItem.DataItem = dataItem;
			}

			OnItemCreated(e);
			Controls.Add(repeaterItem);

			if (!dataBind)
			{
				return repeaterItem;
			}

			repeaterItem.DataBind();
			OnItemDataBound(e);
			repeaterItem.DataItem = null;

			return repeaterItem;
		}

		private static RepeaterItem CreateItem(int itemIndex, ListItemType itemType)
		{
			return new RepeaterItem(itemIndex, itemType);
		}

		private void InitializeItem(RepeaterItem item)
		{
			ITemplate template = null;

			switch (item.ItemType)
			{
				case ListItemType.Header:
					template = HeaderTemplate;
					break;
				case ListItemType.Item:
					template = ItemTemplate;
					break;
				case ListItemType.Footer:
					template = FooterTemplate;
					break;
			}

			template?.InstantiateIn(item);
		}

		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			var flag = false;
			var args = e as RepeaterCommandEventArgs;

			if (args != null)
			{
				OnItemCommand(args);
				flag = true;
			}

			return flag;
		}
	}
}